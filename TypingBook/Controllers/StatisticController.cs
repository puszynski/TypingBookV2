using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TypingBook.Services.IServices;
using TypingBook.ViewModels.Statistic;

namespace TypingBook.Controllers
{
    public class StatisticController : BaseController
    {
        private readonly IStatisticService _statisticsService;

        public StatisticController(IStatisticService statisticsService)
            => (_statisticsService) = (statisticsService);

        public IActionResult Index()
        {
            var userId = GetLoggedUserId();

            if (string.IsNullOrEmpty(userId))
                return NotFound();

            var userData = _statisticsService.GetUserDataById(userId).First();
            var model = new StatisticViewModel() { TypedCorrect = userData.Item2, TypedWrong = userData.Item3 };

            return View(model);
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