using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TypingBook.Helpers;
using TypingBook.Models;
using TypingBook.Repositories.IReporitories;
using TypingBook.ViewModels.Home;
using TypingBook.ViewModels.Typing;

namespace TypingBook.Controllers
{
    public class TypingController : BaseController
    {
        readonly IUserDataRepository _userDataRepository;
        readonly IBookRepository _bookRepository;
        readonly IMemoryCache _memoryCache;

        readonly BookContentHelper _bookContentHelper;

        const int _defaultBook = 1; //book with Id = 1 is Introdutcion for new users

        public TypingController(IUserDataRepository userDataRepository, IBookRepository bookRepository, IMemoryCache memoryCache)
        {
            _userDataRepository = userDataRepository;
            _bookRepository = bookRepository;
            _memoryCache = memoryCache;

            _bookContentHelper = new BookContentHelper();
        }

        // move typing here
        [HttpGet]
        public IActionResult Index(int bookID = _defaultBook, int bookPage = 0)
        {
            var userId = GetLoggedUserId();

            if (userId == null)
            {
                var model = GetIntroductionModel();
                return GetStartPage(); // todo
            }

            // TODO => Move To => GetBookPages():
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

            return null;
        }

        TypingViewModel GetIntroductionModel()
        {
            return new TypingViewModel()
            {
                ...
            }
        }


        [HttpGet]
        [Authorize]
        public IActionResult GetBookProgress()
        {
            var userId = GetLoggedUserId();

            if (userId == null)
                return null;

            var userData = _userDataRepository.GetById(userId);

            var userDataHelper = new UserDataHelper();

            var model = new UserDataViewModel()
            {
                BookProgress = userDataHelper.DeserializeProgressBar(userData.BookProgress),                
                Statistics = userDataHelper.DeserializeStatisticBar(userData.Statistics)
            };

            return Json(userData);
        }

        [HttpPost]
        public IActionResult SaveBookProgress()
        {
            var userId = GetLoggedUserId();

            if (userId == null)
                return null;

            var userData = "";
            

            return Json(userData);
        }
    }
}
