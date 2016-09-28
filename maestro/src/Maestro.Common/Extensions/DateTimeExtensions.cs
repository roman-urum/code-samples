using System;

namespace Maestro.Common.Extensions
{
    /// <summary>
    /// Extension methods for DateTime class.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns count of years between specified and current date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int YearsSince(this DateTime date)
        {
            DateTime today = DateTime.Today;
            int years = today.Year - date.Year;

            if (date > today.AddYears(-years))
            {
                years--;
            }

            return years;
        }
    }
}
