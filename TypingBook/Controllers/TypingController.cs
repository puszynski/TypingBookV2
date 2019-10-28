using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TypingBook.Repositories.IReporitories;
using TypingBook.Services.IServices;
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

        
        [HttpGet]
        public IActionResult Index(int? bookId, int? currentBookPage)
        {
            var result = new TypingViewModel();
            var userId = GetLoggedUserId();
            
            result = _typingServices.GetTypingViewModel(userId, bookId, currentBookPage);

            if (result == null)
                return RedirectToAction("Index","Book");
            
            bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";

            if (isAjaxCall)
                return PartialView("_Index", result);
            else
                return View(result);
        }
        
        
        [HttpPost]
        [Authorize]
        public IActionResult SaveTypingResult(int bookId, int nextBookPage, int correctTyped, int wrongTyped)//bookId pokazuje jakby currentPage+1, nextBookPage jest undifined..
        {
            var userId = GetLoggedUserId();
            
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var result = _typingServices.SaveBookProgress(bookId, nextBookPage, userId);  
            return Ok(result);
        }
    }
}
