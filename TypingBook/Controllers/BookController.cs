using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TypingBook.Data;
using TypingBook.Enums;
using TypingBook.Extensions;
using TypingBook.Helpers;
using TypingBook.Models;
using TypingBook.ViewModels.Book;

namespace TypingBook.Controllers
{
    public class BookController : Controller
    {
        readonly ISQLiteDapperRepository _sqLiteDB;
               
        public BookController(ISQLiteDapperRepository sqLiteDB) => _sqLiteDB = sqLiteDB;
               
        public IActionResult Index(string bookOrCompanySearchString, int? genreFilter)
        {
            var sql = _sqLiteDB.GetAllBooks();

            if (!string.IsNullOrWhiteSpace(bookOrCompanySearchString))
                sql = sql.Where(x => x.Title.Contains(bookOrCompanySearchString)
                                || x.Authors.Contains(bookOrCompanySearchString));

            if (genreFilter.HasValue)
            {
                sql = sql.Where(x => (x.Genre & genreFilter) > 0); // TODO - PRZEANALIZOWAC
            }

            var enumConv = EnumBinarySumConverterHelper.GetInstance();

            var row = sql.Select(x => new BookRowViewModel {
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
            //model.BookGenreSelectListItems = 

            // TODO - if ajax load partial view like in Home/Index
            return View(model);
        }

        public IActionResult Create()
        {
            var model = new BookRowViewModel();
            
            model.GenreSelectListItem = CreateSelectListItemHelper.GetInstance().GetSelectListItems<EBookGenre>();

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
                Content = bookContentHelper.TransformedBookContent(model.Content),
                Genre = enumConv.ParseSelectedListItemsToBinarySum(model.GenreSelectListItem),
                Title = model.Title,
                ReleaseDate = model.ReleaseDate.HasValue ? model.ReleaseDate : null,
            };

            //_sqLiteDB.Create(sql);

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var sql = _sqLiteDB.GetBookByID(id);

            if (sql == null)
                return RedirectToAction("Index");

            var enumConv = EnumBinarySumConverterHelper.GetInstance();

            // DLA POSTA I CREATEA POSTA*
            //REMOVEDUBLESPACESANDSPECIALCHARACTERSFROMBOOKCONTENT
            //    myString = Regex.Replace(myString, @"\s+", " "); //Since it will catch runs of any kind of whitespace (e.g. tabs, newlines, etc.) and replace them with a single space.

            var model = new BookRowViewModel
            {
                ID = sql.ID,
                Title = sql.Title,
                Content = sql.Content,
                Genre = sql.Genre.HasValue ? enumConv.ParseBinarySumToIntList(sql.Genre.GetValueOrDefault()).ToList() : null,
                GenreSelectListItem = CreateSelectListItemHelper.GetInstance().GetSelectListItems<EBookGenre>(),
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

            var sql = _sqLiteDB.GetBookByID(model.ID);


            var bookContentHelper = new BookContentHelper();
            sql.Content = bookContentHelper.TransformedBookContent(model.Content);
            sql.Authors = model.Authors;
            sql.ReleaseDate = model.ReleaseDate;
            sql.Title = model.Title;

            var enumConv = EnumBinarySumConverterHelper.GetInstance();
            sql.Genre = enumConv.ParseSelectedListItemsToBinarySum(model.GenreSelectListItem);
            
            //TODO - repo
            //_sqLiteDB.UpdateBook(sql);

            return RedirectToAction("Index");
        }
    }
}
