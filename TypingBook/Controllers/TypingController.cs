using Microsoft.AspNetCore.Mvc;
using TypingBook.Repositories.IReporitories;
using TypingBook.ViewModels.Typing;

namespace TypingBook.Controllers
{
    public class TypingController : BaseController
    {
        readonly IUserDataRepository _userDataRepository;

        public TypingController(IUserDataRepository userDataRepository)
        {
            _userDataRepository = userDataRepository;
        }

        // move typing here


        [HttpGet]
        public IActionResult GetBookProgress()
        {
            var userId = GetLoggedUserId();

            if (userId == null)
                return null;

            var userData = _userDataRepository.GetById(userId);

            var model = new UserDataViewModel()
            {
                BookProgress = userData.BookProgress,
                Statistics = userData.Statistics,
            }

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
