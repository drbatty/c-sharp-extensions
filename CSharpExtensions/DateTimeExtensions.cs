using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpExtensions
{
    public static class DateTimeExtensions
    {
        
        public static bool IsWeekend(this DateTime value)
        {
            return value.DayOfWeek.In(DayOfWeek.Saturday, DayOfWeek.Sunday);
        }

        public static string FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1).ToString("dd/MM/yyyy");
        }

        public static string LastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month + 1, 1).AddDays(-1).ToString("dd/MM/yyyy");
        }


        public static bool IsFuture(this DateTime date, DateTime from)
        {
            return date.Date > from.Date;
        }

        public static bool IsPast(this DateTime date, DateTime from)
        {
            return date.Date < from.Date;
        }

        public static string MonthName(int month)
        {
            if (month < 1 || month > 12)
                return string.Empty;
            return CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(month);
        }
    }
}
