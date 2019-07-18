using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TypingBook.Helpers;
using TypingBook.Repositories.IReporitories;
using TypingBook.Services.IServices;
using TypingBook.ViewModels.Home;
using TypingBook.ViewModels.Typing;

namespace TypingBook.Controllers
{
    public class TypingController : BaseController
    {
        readonly IUserDataRepository _userDataRepository;
        readonly IBookRepository _bookRepository;
        readonly IMemoryCache _memoryCache;
        readonly ITypingServices _typingServices;

        public TypingController(IUserDataRepository userDataRepository, IBookRepository bookRepository, ITypingServices typingServices, IMemoryCache memoryCache)
        {
            _userDataRepository = userDataRepository;
            _bookRepository = bookRepository;
            _memoryCache = memoryCache;
            _typingServices = typingServices;
        }

        // move typing here
        [HttpGet]
        public IActionResult Index(int? bookId, int? currentBookPage)
        {
            var result = new TypingViewModel();

            if (bookId.HasValue && _memoryCache.TryGetValue($"Book_ID{bookId}", out TypingViewModel book))
                result = book; // warunek: aby poprawnie działało => aktualizuj cache po każdej stronie! (w akcji zapisywania progresu)
            else
            {
                var userId = GetLoggedUserId();
                result = _typingServices.GetTypingViewModel(userId, bookId, currentBookPage);
            }
            
            bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";

            if (isAjaxCall)
                return PartialView("_Index", result);
            else
                return View(result);
        }
               

        [HttpGet]
        [Authorize]
        public IActionResult GetBookProgress(TypingViewModel model)
        {
            var userId = GetLoggedUserId();

            if (_memoryCache.TryGetValue($"Book_ID{bookId}", out TypingViewModel book))
            {
                book.currentBookPage = currentBookPage;
            }
            else
            {
                var cacheEntry = model;
                _cache.Set($"Book_ID{bookId}", cacheEntry, cacheEntryOptions);

            }
            //save in cache => https://docs.microsoft.com/pl-pl/aspnet/core/performance/caching/memory?view=aspnetcore-2.2

            var cacheEntry = DateTime.Now;
            _memoryCache.Save;


            //save in db
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

        // TODO WYWOŁAĆ TĄ AKCJE Z JS PODCZAS TYPING
        [HttpPost]
        public IActionResult SaveTypingResult(int bookId, int typedBookPage)
        {
            var userId = GetLoggedUserId();



            // TODO - aktualizuj cache po każdej stronie!

            if (!string.IsNullOrEmpty(userId))
                _typingServices.SaveBookProgress(userId, bookId, typedBookPage); //TODO use boolen

            return Ok();
        }

        [Authorize]
        private SaveBookProgressInDB()
        {

        }
    }
}
