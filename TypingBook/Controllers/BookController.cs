using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TypingBook.Data;
using TypingBook.Enums;
using TypingBook.Extensions;
using TypingBook.Helpers;
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

            var enumConv = BinarySumToIntListHelper.GetInstance();

            var row = sql.Select(x => new BookRowViewModel {
                ID = x.ID,
                Title = x.Title,
                Content = x.Content.ShowOnly500Char(),
                Authors = x.Authors,
                Genre = x.Genre.HasValue ? enumConv.Parse(x.Genre.GetValueOrDefault()).ToList() : null,
                Rate = x.Rate,
                ReleaseDate = x.ReleaseDate
            });

            var model = new BookViewModel(row);            
            model.BookGenreSelectListItems = CreateSelectListItemHelper.GetInstance().GetSelectListItems<EBookGenre>();
            
            return View(model);
        }

        public IActionResult Create()
        {
            var model = new BookViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(BookViewModel model)
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var sql = _sqLiteDB.GetBookByID(id);

            if (sql == null)
                return RedirectToAction("Index");

            var enumConv = BinarySumToIntListHelper.GetInstance();

            var model = new BookRowViewModel
            {
                ID = sql.ID,
                Title = sql.Title,
                Content = sql.Content,
                Genre = sql.Genre.HasValue ? enumConv.Parse(sql.Genre.GetValueOrDefault()).ToList() : null,
                Authors = sql.Authors,
                Rate = sql.Rate,
                ReleaseDate = sql.ReleaseDate
            };
            return RedirectToAction("Index");
        }
    }
}
