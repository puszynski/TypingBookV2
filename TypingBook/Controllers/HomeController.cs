using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TypingBook.Data;
using TypingBook.Helpers;
using TypingBook.Models;
using TypingBook.ViewModels.Home;

namespace TypingBook.Controllers
{
    public class HomeController : Controller
    {
        private const int _defaultBook = 2;
        readonly ISQLiteDapperRepository _sqLiteDB;

        public HomeController(ISQLiteDapperRepository sqLiteDB) => _sqLiteDB = sqLiteDB;


        public IActionResult Index(int bookID = _defaultBook, int bookPage = 0)
        {
            var book = _sqLiteDB.GetBookByID(bookID);

            if (bookID == 1)
                ViewBag.IsIntroduction = true;

            var typingHelper = new TypingHelper();
            var bookPages = typingHelper.DivideBook(book.Content);            

            var model = new TypingViewModel()
            {
                BookAuthors = book.Authors,
                CurrentBookPage = bookPage,
                BookPages = bookPages,
                BookTitle = book.Title,
                BookID = bookID
            };


            bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (isAjaxCall)
                return PartialView("_Index", model);
            else
                return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
