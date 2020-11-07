using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

            var allStats = _statisticsService.GetUserDataById(userId);
            var lastThreeMonthsStats = allStats.OrderByDescending(x => x.date).Take(3);

            var monthlyStatistic = new List<MonthlyStatisticViewModel>();

            if (lastThreeMonthsStats.Any())
            {
                monthlyStatistic.Add(new MonthlyStatisticViewModel() 
                { 
                    Date = lastThreeMonthsStats.First().date,
                    TypedCorrect = lastThreeMonthsStats.First().typedCorrect,
                    TypedWrong = lastThreeMonthsStats.First().typedWrong
                });

                if (lastThreeMonthsStats.Count() >= 2)
                {
                    monthlyStatistic.Add(new MonthlyStatisticViewModel()
                    {
                        Date = lastThreeMonthsStats.ElementAt(1).date,
                        TypedCorrect = lastThreeMonthsStats.ElementAt(1).typedCorrect,
                        TypedWrong = lastThreeMonthsStats.ElementAt(1).typedWrong
                    });
                }

                if (lastThreeMonthsStats.Count() == 3)
                {
                    monthlyStatistic.Add(new MonthlyStatisticViewModel()
                    {
                        Date = lastThreeMonthsStats.Last().date,
                        TypedCorrect = lastThreeMonthsStats.Last().typedCorrect,
                        TypedWrong = lastThreeMonthsStats.Last().typedWrong
                    });
                }
            }

            var model = new StatisticViewModel() 
            { 
                TypedCorrect = allStats.Sum(x => x.typedCorrect),
                TypedWrong = allStats.Sum(x => x.typedWrong),
                MonthlyStatistic = monthlyStatistic
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveData(int typedCorrect, int typedWrong, int millisecondsOfTyping)
        {
            var userId = GetLoggedUserId();

            if (string.IsNullOrEmpty(userId))
                return NotFound();

            _statisticsService.SaveDataByUserId(userId, typedCorrect, typedWrong, millisecondsOfTyping);

            return Ok();
        }
    }
}