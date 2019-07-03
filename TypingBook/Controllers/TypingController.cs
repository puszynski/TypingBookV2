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
            var model = new TypingViewModel();

            if (bookId.HasValue && _memoryCache.TryGetValue($"Book_ID{bookId}", out TypingViewModel book))
                model = book; /// warunek: aby poprawnie działało => aktualizuj cache po każdej stronie! (w akcji zapisywania progresu) 


            var userId = GetLoggedUserId();

            if (userId == null)
                model = _typingServices.GetIntroductionModel(bookId, currentBookPage);
            else
                model = _typingServices.GetTypingBookModel(userId, bookId, currentBookPage);

            //CO JAK KTOŚ NIE BEDZIE UZYWAC CACHE??!! - zakładam że każdy musi (że nie da się tego zblokować)

            bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";

            if (isAjaxCall)
                return PartialView("_Index", model);
            else
                return View(model);
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

            // TODO - aktualizuj cache po każdej stronie!

            if (userId == null)
                return null;

            var userData = "";
            

            return Json(userData);
        }
    }
}
