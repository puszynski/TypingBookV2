using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TypingBook.Enums;
using TypingBook.Extensions;
using TypingBook.Helpers;
using TypingBook.Models;
using TypingBook.Repositories.IReporitories;
using TypingBook.Services;
using TypingBook.ViewModels.Book;

namespace TypingBook.Controllers
{
    public class BookController : BaseController
    {
        readonly IBookRepository _bookRepository;
        readonly ILogger _logger;

        public BookController(IBookRepository bookRepository, ILogger<BookController> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public IActionResult Index(string bookOrAuthorSearchString, int? genreFilter)
        {
            IQueryable<Book> sql;

            if (IsLoggerdUserAdministrator())
                sql = _bookRepository.GetAllBooks();
            else
                sql = _bookRepository.GetAllBooks().Where(x => x.IsVerified == true);            

            //TODO FILTR NIE DZIAŁA ^^`
            if (!string.IsNullOrWhiteSpace(bookOrAuthorSearchString))
                sql = sql.Where(x => x.Title.Contains(bookOrAuthorSearchString)
                                || x.Authors.Contains(bookOrAuthorSearchString));

            if (genreFilter.HasValue)
                sql = sql.Where(x => (x.Genre & genreFilter) > 0);
            
            var row = sql.ToList().Select(x => new BookRowViewModel
            {
                ID = x.Id,
                Title = x.Title,
                Content = x.Content.ShowOnly500Char(),
                Authors = x.Authors,
                Genre = x.Genre.HasValue ? x.Genre.Value.ConvertEnumSumToIntArray().ToList() : null,
                Rate = x.Rate,
                ReleaseDate = x.ReleaseDate,
                AddDate = x.AddDate,
                IsVerified = x.IsVerified,
                License = x.License
            });

            var model = new BookViewModel(row);
            model.BookGenreSelectListItems = CreateSelectListItemHelper.GetInstance().GetSelectListItems<EBookGenre>();

            // TODO - if ajax load partial view like in Home/Index
            return View(model);
        }
        
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

            try
            {
                var bookService = new BookContentService();

                var sql = new Book
                {
                    Authors = model.Authors,
                    Content = bookService.TransformeBookContent(model.Content),
                    Genre = model.Genre.Sum(),
                    Title = model.Title,
                    ReleaseDate = model.ReleaseDate.HasValue ? model.ReleaseDate : null,
                    AddDate = DateTime.Now,
                    License = model.License,
                    IsVerified = IsLoggerdUserAdministrator() ? true : false,
                    UserId = GetLoggedUserId()
                };

                _bookRepository.CreateBook(sql);
                _bookRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR => BookController/Create => " + ex);
            }
            

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id) 
        {
            // mozesz edytowac tylko swoje ksiazki chyba ze jestes adminem, to wszystkie
            // jesli nie jestes uzytkownikiem - nic nie mozesz
            var sql = _bookRepository.GetBookByID(id);

            if (sql == null)
                return RedirectToAction("Index");

            var enumConv = EnumBinarySumConverterHelper.GetInstance();

            var model = new BookRowViewModel
            {
                ID = sql.Id,
                Title = sql.Title,
                Content = sql.Content,
                Genre = sql.Genre.HasValue ? sql.Genre.Value.ConvertEnumSumToIntArray().ToList() : null,
                Authors = sql.Authors,
                Rate = sql.Rate,
                ReleaseDate = sql.ReleaseDate,
                AddDate = sql.AddDate,
                IsVerified = IsLoggerdUserAdministrator() ? true : sql.IsVerified,
                License = sql.License,
                UserId = sql.UserId
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

            sql.Content = bookService.TransformeBookContent(model.Content);
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
    }
}
