using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TypingBook.Extensions;
using TypingBook.Helpers;
using TypingBook.Models;
using TypingBook.Repositories.IReporitories;
using TypingBook.Services;
using TypingBook.ViewModels.Book;
using TypingBook.ViewModelsBuilders.Book;

namespace TypingBook.Controllers
{
    public class BookController : BaseController
    {
        readonly IBookRepository _bookRepository;
        readonly IUserDataRepository _userDataRepository;

        public BookController(IBookRepository bookRepository, IUserDataRepository userDataRepository)
            => (_bookRepository, _userDataRepository) = (bookRepository, userDataRepository);

        [HttpGet]
        public IActionResult Index(string bookOrAuthorSearchString, int? genreFilter)
        {
            var bookViewModelBuilder = new BookViewModelBuilder(_bookRepository,
                                                                _userDataRepository,
                                                                GetLoggedUserId(),
                                                                IsLoggerdUserAdministrator(),
                                                                bookOrAuthorSearchString,
                                                                genreFilter);

            // TODO - if ajax load partial view like in Home/Index
            return View(bookViewModelBuilder.Build());            
        }                

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            var model = new BookRowViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(BookRowViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var bookService = new BookContentService();

            model.ContentBeforeModification = model.Content;

            try
            {
                model.Content = bookService.CreateBookPagesJSON(model.Content);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;                
                return View(model);
            }

            var sql = new Book
            {
                Authors = model.Authors,
                Content = model.Content,
                Genre = model.Genre?.Sum(),
                Description = model.Description,
                Title = model.Title,
                ReleaseDate = model.ReleaseDate.HasValue ? model.ReleaseDate : null,
                AddDate = DateTime.UtcNow,
                License = model.License,
                IsVerified = false,
                UserId = GetLoggedUserId(),
                ContentBeforeModifying = model.ContentBeforeModification
            };

            _bookRepository.CreateBook(sql);
            _bookRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult RebuildBookPages(int id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var book = _bookRepository.GetBookByID(id);
            if (book == null && string.IsNullOrEmpty(book.ContentBeforeModifying) && book.IsVerified)
                return RedirectToAction("Index");//todo show info

            var bookService = new BookContentService();
            book.Content = bookService.CreateBookPagesJSON(book.ContentBeforeModifying);
            _bookRepository.SaveChanges();

            return RedirectToAction("Index");//TODO REDIRECT TO RETRUN URL
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]//todo - mozesz edytowac tylko swoje ksiazki chyba ze jestes adminem, to wszystkie - jesli nie jestes uzytkownikiem - nic nie mozesz
        public IActionResult Edit(int id) 
        {
            var sql = _bookRepository.GetBookByID(id);

            if (sql == null)
                return RedirectToAction("Index");

            var enumConv = EnumBinarySumConverterHelper.GetInstance();

            var model = new BookRowViewModel
            {
                ID = sql.Id,
                Title = sql.Title,
                Description = sql.Description,
                Content = sql.Content,
                ContentInBookPages = JsonSerializer.Deserialize<List<string>>(sql.Content),//edytowanie nic nie da bo prześlesz i tak content
                Genre = sql.Genre.HasValue ? sql.Genre.Value.ConvertEnumSumToIntArray().ToList() : null,
                Authors = sql.Authors,
                Rate = sql.Rate,
                ReleaseDate = sql.ReleaseDate,
                AddDate = sql.AddDate,
                IsVerified = sql.IsVerified,
                License = sql.License,
                UserId = sql.UserId,
                ContentBeforeModification = sql.ContentBeforeModifying
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(BookRowViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var sql = _bookRepository.GetBookByID(model.ID);

            var bookService = new BookContentService();

            sql.Content = bookService.CreateBookPagesJSON(model.Content);
            sql.Authors = model.Authors;
            sql.ReleaseDate = model.ReleaseDate;
            sql.Title = model.Title;
            sql.Genre = model.Genre.Sum();

            _bookRepository.UpdateBook(sql);
            _bookRepository.SaveChanges();

            return RedirectToAction("Index");
        }
                
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var book = await _bookRepository.GetAsyncBookByID(id.Value);

            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetAsyncBookByID(id);
            _bookRepository.RemoveBook(book);
            await _bookRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> VerifyBook(int id)
        {
            var book = await _bookRepository.GetAsyncBookByID(id);
            book.IsVerified = true;
            await _bookRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
