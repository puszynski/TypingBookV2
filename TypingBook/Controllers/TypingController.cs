using System;
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
        

        // TODO call from js
        [HttpPost]
        public IActionResult SaveTypingResult(int correctTyped, int wrongTyped, TypingViewModel model = null)
        {
            var userId = GetLoggedUserId();
            
            if (model == null || string.IsNullOrEmpty(userId))
                return BadRequest();

            if (_memoryCache.TryGetValue($"Book{model.BookId}User{userId}", out TypingViewModel book))
            {
                _memoryCache.Remove($"Book{model.BookId}User{userId}");
                _memoryCache.Set(
                    $"Book{model.BookId}User{userId}", 
                    model, 
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(90))
                    );                
            }
            else
                _memoryCache.Set($"Book{model.BookId}User{userId}", model);

            var result = _typingServices.SaveBookProgress(model, userId);  
            return Ok(result);
        }
    }
}
