using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TypingBook.Enums;
using TypingBook.Extensions;
using TypingBook.Helpers;
using TypingBook.Models;
using TypingBook.Repositories.IReporitories;
using TypingBook.ViewModels.Book;

namespace TypingBook.Controllers
{
    public class BookController : BaseController
    {
        readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IActionResult Index(string bookOrAuthorSearchString, int? genreFilter)
        {
            var sql = _bookRepository.GetAllBooks();

            // todo - get loggged user
            //var user = User... if user == null ..... else ...


            if (!string.IsNullOrWhiteSpace(bookOrAuthorSearchString))
                sql = sql.Where(x => x.Title.Contains(bookOrAuthorSearchString)
                                || x.Authors.Contains(bookOrAuthorSearchString));

            if (genreFilter.HasValue)
                sql = sql.Where(x => (x.Genre & genreFilter) > 0);


            var row = sql.Select(x => new BookRowViewModel
            {
                ID = x.Id,
                Title = x.Title,
                Content = x.Content.ShowOnly500Char(),
                Authors = x.Authors,
                Genre = x.Genre.HasValue ? x.Genre.Value.ConvertEnumSumToIntArray().ToList() : null,
                Rate = x.Rate,
                ReleaseDate = x.ReleaseDate
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

            var bookContentHelper = new BookContentHelper();
            var enumConv = EnumBinarySumConverterHelper.GetInstance();

            var sql = new Book
            {
                Authors = model.Authors,
                Content = bookContentHelper.TransformeBookContent(model.Content),
                Genre = model.Genre.Sum(),
                Title = model.Title,
                ReleaseDate = model.ReleaseDate.HasValue ? model.ReleaseDate : null,
            };

            _bookRepository.CreateBook(sql);
            _bookRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
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
                Genre = sql.Genre.HasValue ? sql.Genre.Value.ConvertEnumSumToIntArray().ToList() : null,
                Authors = sql.Authors,
                Rate = sql.Rate,
                ReleaseDate = sql.ReleaseDate
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

            var bookContentHelper = new BookContentHelper();
            sql.Content = bookContentHelper.TransformeBookContent(model.Content);
            sql.Authors = model.Authors;
            sql.ReleaseDate = model.ReleaseDate;
            sql.Title = model.Title;
            sql.Genre = model.Genre.Sum();

            _bookRepository.UpdateBook(sql);
            _bookRepository.SaveChanges();

            return RedirectToAction("Index");
        }
                
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _bookRepository.GetAsyncBookByID(id);
            _bookRepository.RemoveBook(book);
            await _bookRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
