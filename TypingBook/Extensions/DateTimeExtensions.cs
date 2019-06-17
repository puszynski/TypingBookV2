using System;

namespace TypingBook.Extensions
{
    public static class DateTimeExtensions
    {
        // Where(x => x.ChangeDate >= from.ToReportStartDate 
        //         && x.ChangeDate < to.ToReportEndDate) 

        public static DateTime ToReportStartDate(this DateTime date)
        {
            return date.Date;
        }

        public static DateTime ToReportEndDate(this DateTime date)
        {
            return date.Date.AddDays(1);
        }
    }
}
