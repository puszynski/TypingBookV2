using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypingBook.ViewModels.Typing
{
    public class UserDataViewModel
    {
        /// <summary>
        /// Dictionary<BookId,BookPage>
        /// </summary>
        public Dictionary<int, int> BookProgress { get; set; }

        /// <summary>
        /// Dictionary<PeriodDateTime,CorrectPercentage> where CorrectPercentage range from 0 to 100
        /// </summary>
        public Dictionary<DateTime, int> Statistics { get; set; }
    }
}
