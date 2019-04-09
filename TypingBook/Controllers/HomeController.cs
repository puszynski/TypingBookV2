using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using TypingBook.Data;
using TypingBook.Helpers;
using TypingBook.Models;
using TypingBook.ViewModels.Home;

namespace TypingBook.Controllers
{
    public class HomeController : BaseController
    {   
        readonly ISQLiteDapperRepository _sqLiteDB;
        readonly IMemoryCache _memoryCache;

        private const int _defaultBook = 2;

        public HomeController(ISQLiteDapperRepository sqLiteDB, IMemoryCache memoryCache)
        {
            _sqLiteDB = sqLiteDB;
            _memoryCache = memoryCache;
        }

        // Typing
        public IActionResult Index(int bookID = _defaultBook, int bookPage = 0)
        {
            if (!_memoryCache.TryGetValue<Book>($"Book_ID{bookID}", out Book book))
            {
                book = _sqLiteDB.GetBookByID(bookID);
                _memoryCache.Set<Book>($"Book_ID{bookID}", book);
            }            

            if (bookID == 1)
                ViewBag.IsIntroduction = true;

            var typingHelper = TypingHelper.GetInstance();
            var bookPages = typingHelper.DivideBook(book.Content);

            // tutaj sprawdzasz czy istnieje cash dla danego usera - jak tak to jest tam podana strona dla ktorej skonczył - musimy dodatkowo wywołać akcje z js po każdej stronie i za pomocą tej akcji zapisywać/aktualizować aktualną stone;
            int? bookPageFormCache = _memoryCache.Get<int>("UserIdBookId");
            

            var model = new TypingViewModel()
            {
                BookAuthors = book.Authors,
                CurrentBookPage = bookPageFormCache.HasValue ? bookPageFormCache.Value : bookPage,
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
            bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";
            
            if (isAjaxCall)
                return PartialView("_Privacy");
            else
                return View();
        }

        public IActionResult ModalPrivacy()
        {
            // do obsługi modala
            return View("ModalPrivacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
