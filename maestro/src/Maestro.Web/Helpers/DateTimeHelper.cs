using System;
using System.Globalization;

using Maestro.Web.Extensions;

namespace Maestro.Web.Helpers
{
    /// <summary>
    /// DateTimeHelper.
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Parses the preferred session time.
        /// </summary>
        /// <param name="strTimeSpan">The string time span.</param>
        /// <returns></returns>
        public static TimeSpan ParsePreferredSessionTime(string strTimeSpan)
        {
            DateTime parseDateTime;
            if (DateTime.TryParseExact(strTimeSpan,"hh:mm tt",CultureInfo.InvariantCulture,DateTimeStyles.None,out parseDateTime))
            {
                return parseDateTime.TimeOfDay;
            }
            
            if (DateTime.TryParseExact(strTimeSpan, "hh:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out parseDateTime))
            {
                return parseDateTime.TimeOfDay;
            }      
     
            if (DateTime.TryParseExact(strTimeSpan, "H:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out parseDateTime))
            {
                return parseDateTime.TimeOfDay;
            }      

            return parseDateTime.TimeOfDay;
        }

        public static DateTime Min(DateTime d1, DateTime d2)
        {
            return d1 < d2 ? d1 : d2;
        }

        public static DateTime? Min(DateTime? d1, DateTime? d2)
        {
            return d1.LessOrEqualThan(d2) ? d1 : d2;
        }

        public static DateTime Max(DateTime d1, DateTime d2)
        {
            return d1 > d2 ? d1 : d2;
        }

        public static DateTime? Max(DateTime? d1, DateTime? d2)
        {
            return d1.GreaterOrEqualThan(d2) ? d1 : d2;
        }
    }
}