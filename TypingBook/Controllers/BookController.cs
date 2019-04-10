using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        public IActionResult Index(string bookOrCompanySearchString, int? genreFilter)
        {
            var sql = _bookRepository.GetAllBooks();

            if (!string.IsNullOrWhiteSpace(bookOrCompanySearchString))
                sql = sql.Where(x => x.Title.Contains(bookOrCompanySearchString)
                                || x.Authors.Contains(bookOrCompanySearchString));

            if (genreFilter.HasValue)
                sql = sql.Where(x => (x.Genre & genreFilter) > 0);

            var enumConv = EnumBinarySumConverterHelper.GetInstance();

            var row = sql.Select(x => new BookRowViewModel
            {
                ID = x.ID,
                Title = x.Title,
                Content = x.Content.ShowOnly500Char(),
                Authors = x.Authors,
                Genre = x.Genre.HasValue ? enumConv.ParseBinarySumToIntList(x.Genre.GetValueOrDefault()).ToList() : null,
                Rate = x.Rate,
                ReleaseDate = x.ReleaseDate
            });

            var model = new BookViewModel(row);
            model.BookGenreSelectListItems = CreateSelectListItemHelper.GetInstance().GetSelectListItems<EBookGenre>();

            // TODO - if ajax load partial view like in Home/Index
            return View(model);
        }

        public IActionResult Create()
        {
            var model = new BookRowViewModel();
            return View(model);
        }

        [HttpPost]
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
                Genre = enumConv.ParseSelectedListItemsToBinarySum(model.GenreSelectListItem),
                Title = model.Title,
                ReleaseDate = model.ReleaseDate.HasValue ? model.ReleaseDate : null,
            };

            _bookRepository.CreateBook(sql);
            _bookRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var sql = _bookRepository.GetBookByID(id);

            if (sql == null)
                return RedirectToAction("Index");

            var enumConv = EnumBinarySumConverterHelper.GetInstance();

            var model = new BookRowViewModel
            {
                ID = sql.ID,
                Title = sql.Title,
                Content = sql.Content,
                Genre = sql.Genre.HasValue ? enumConv.ParseBinarySumToIntList(sql.Genre.GetValueOrDefault()).ToList() : null,
                Authors = sql.Authors,
                Rate = sql.Rate,
                ReleaseDate = sql.ReleaseDate
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(BookRowViewModel model)
        {
            if (ModelState.IsValid)
                return View(model);

            var sql = _bookRepository.GetBookByID(model.ID);

            var bookContentHelper = new BookContentHelper();
            sql.Content = bookContentHelper.TransformeBookContent(model.Content);
            sql.Authors = model.Authors;
            sql.ReleaseDate = model.ReleaseDate;
            sql.Title = model.Title;

            var enumConv = EnumBinarySumConverterHelper.GetInstance();
            sql.Genre = enumConv.ParseSelectedListItemsToBinarySum(model.GenreSelectListItem);

            _bookRepository.UpdateBook(sql);
            _bookRepository.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
