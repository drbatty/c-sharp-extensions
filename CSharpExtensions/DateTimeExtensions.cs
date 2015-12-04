using System;
using System.Globalization;
using CSharpExtensions.DependencyInjection;

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

        public static int Age(this DateTime dateOfBirth)
        {
            var now = Providers.TimeService.Now();
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age;
        }

        public static string ToFriendlyDateString(this DateTime date)
        {
            string formattedDate;
            var today = Providers.TimeService.Today();

            if (date.Date == today)
                formattedDate = "Today";

            else if (date.Date == today.AddDays(-1))
                formattedDate = "Yesterday";

            else if (date.Date > today.AddDays(-6))
                formattedDate = date.ToString("dddd");

            else
                formattedDate = date.ToString("MMMM dd, yyyy");

            formattedDate += " @ " + date.ToString("t").ToLower();
            return formattedDate;
        }

        public static bool IsFuture(this DateTime date)
        {
            return date.IsFuture(Providers.TimeService.Now());
        }

        public static bool IsPast(this DateTime date)
        {
            return date.IsPast(Providers.TimeService.Now());
        }
    }
}
