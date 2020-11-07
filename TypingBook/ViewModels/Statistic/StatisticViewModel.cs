using System;
using System.Collections.Generic;

namespace TypingBook.ViewModels.Statistic
{
    public class StatisticViewModel
    {
        public int TypedCorrect { get; set; } = 50;
        public int TypedWrong { get; set; } = 20;
        public List<MonthlyStatisticViewModel> MonthlyStatistic { get; set; }
    }

    public class MonthlyStatisticViewModel
    {
        public DateTime Date { get; set; }
        public int TypedCorrect { get; set; }
        public int TypedWrong { get; set; }
    }
}
