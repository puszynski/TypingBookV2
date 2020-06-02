using Microsoft.AspNetCore.Mvc;
using TypingBook.Services.IServices;

namespace TypingBook.Controllers
{
    public class StatisticsController : BaseController
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService) 
            => (_statisticsService) = (statisticsService);

        public IActionResult Index()
        {
            var userId = GetLoggedUserId();

            if (string.IsNullOrEmpty(userId))
                return NotFound();

            var userData = _statisticsService.GetUserDataById(userId);
            return View(userData);
        }

        [HttpPost]
        public IActionResult SaveData(int typedCorrect, int typedWrong, int secondsOfTyping)
        {
            var userId = GetLoggedUserId();

            if (string.IsNullOrEmpty(userId))
                return NotFound();

            _statisticsService.SaveDataByUserId(userId, typedCorrect, typedWrong, secondsOfTyping);

            return Ok();
        }
    }
}