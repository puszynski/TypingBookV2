using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        private readonly IBookRepository _bookRepository;
        private readonly IUserDataRepository _userDataRepository;

        public BookController(IBookRepository bookRepository, IUserDataRepository userDataRepository)
        {
            _bookRepository = bookRepository;
            _userDataRepository = userDataRepository;
        }

        public IActionResult Index(string bookOrAuthorSearchString, int? genreFilter)
        {
            var userId = GetLoggedUserId();
            var sql = GetBaseQuery(userId);            

            //TODO FILTR NIE DZIAŁA ^^`
            if (!string.IsNullOrWhiteSpace(bookOrAuthorSearchString))
                sql = sql.Where(x => x.Title.Contains(bookOrAuthorSearchString)
                                || x.Authors.Contains(bookOrAuthorSearchString));

            if (genreFilter.HasValue)
                sql = sql.Where(x => (x.Genre & genreFilter) > 0);
            
            var bookRowViewModels = sql
                .ToList()
                .Select(x => new BookRowViewModel
                {
                    ID = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Authors = x.Authors,
                    Genre = x.Genre.HasValue ? x.Genre.Value.ConvertEnumSumToIntArray().ToList() : null,
                    Rate = x.Rate,
                    ReleaseDate = x.ReleaseDate,
                    AddDate = x.AddDate,
                    IsVerified = x.IsVerified,
                    License = x.License
                })
                .ToList();

            AssignUserLastTypedPage(bookRowViewModels, userId);

            var model = new BookViewModel(bookRowViewModels);
            model.BookGenreSelectListItems = CreateSelectListItemHelper.GetInstance().GetSelectListItems<EBookGenre>();

            // TODO - if ajax load partial view like in Home/Index
            return View(model);
        }

        private IQueryable<Book> GetBaseQuery(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return _bookRepository.GetAllBooks().Where(x => x.IsVerified);

            else if (IsLoggerdUserAdministrator())
                return _bookRepository.GetAllBooks();

            else
                return _bookRepository.GetAllBooks().Where(x => x.IsVerified && !x.IsPrivate || x.UserId == userId);
        }

        private void AssignUserLastTypedPage(List<BookRowViewModel> bookRowViewModels, string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return;
            
            var userBooksInProgress = _userDataRepository.GetByIdUserLastTypedPages(userId);

            foreach (var item in bookRowViewModels)
                    item.UserLastTypedPage = userBooksInProgress.SingleOrDefault(x => x.bookID == item.ID).userLastPage;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            var model = new BookRowViewModel();
            return View(model);
        }

        //zamiast GET => POST => POST przeładuj treść przyciskiem "załaduj / konwerttuj" ajaxem?

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(BookRowViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var bookService = new BookContentService();

            model.ContentBeforeModification = model.Content;
            model.Content = bookService.CreateBookPagesJSON(model.Content);

            var sql = new Book
            {
                Authors = model.Authors,
                Content = model.Content,
                Genre = model.Genre?.Sum(),
                Description = model.Description,
                Title = model.Title,
                ReleaseDate = model.ReleaseDate.HasValue ? model.ReleaseDate : null,
                AddDate = DateTime.Now,
                License = model.License,
                IsVerified = false,
                UserId = GetLoggedUserId(),
                ContentBeforeModifying = model.ContentBeforeModification
            };

            _bookRepository.CreateBook(sql);
            _bookRepository.SaveChanges();

            return RedirectToAction("Index");
        }

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
    }
}
