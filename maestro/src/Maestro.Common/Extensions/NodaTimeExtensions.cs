using System;
using NodaTime;

namespace Maestro.Common.Extensions
{
    /// <summary>
    /// NodaTimeExtensions.
    /// </summary>
    public static class NodaTimeExtensions
    {
        /// <summary>
        /// Greaters the or equal than.
        /// </summary>
        /// <param name="timeZoneId">The time zone identifier.</param>
        /// <param name="momentUtc">The moment UTC.</param>
        /// <returns></returns>
        public static string GetShortTimezoneName(this string timeZoneId, DateTime momentUtc)
        {
            momentUtc = DateTime.SpecifyKind(momentUtc, DateTimeKind.Utc);

            Instant instant = Instant.FromDateTimeUtc(momentUtc);

            var timezone = DateTimeZoneProviders.Tzdb[timeZoneId];

            var zoneInterval = timezone.GetZoneInterval(instant);

            return zoneInterval.Name;
        }

        /// <summary>
        /// Converts the time to UTC (using IANA (Olson) timezone).
        /// </summary>
        /// <param name="local">The local.</param>
        /// <param name="timezone">IANA (Olson) timezone.</param>
        /// <returns></returns>
        public static DateTime ConvertTimeToUtc(this DateTime local, string timezone)
        {
            LocalDateTime localDateTime = LocalDateTime.FromDateTime(local);

            var timezoneInfo = DateTimeZoneProviders.Tzdb[timezone];
            
            var zoned = timezoneInfo.AtLeniently(localDateTime);

            return zoned.ToDateTimeUtc();
        }

        /// <summary>
        /// Converts the time from UTC (using IANA (Olson) timezone).
        /// </summary>
        /// <param name="targetUtc">The target UTC.</param>
        /// <param name="timezone">IANA (Olson) timezone.</param>
        /// <returns></returns>
        public static DateTimeOffset ConvertTimeFromUtc(
            this DateTime targetUtc,
            string timezone
        )
        {
            targetUtc = DateTime.SpecifyKind(targetUtc, DateTimeKind.Utc);

            Instant instant = Instant.FromDateTimeUtc(targetUtc);

            DateTimeZone zoneInfo = DateTimeZoneProviders.Tzdb[timezone];

            ZonedDateTime zoned = instant.InZone(zoneInfo);

            // Removing seconds from the OffSet.
            // More details: https://github.com/nodatime/nodatime/issues/395
            TimeSpan timeSpanOriginal = zoned.Offset.ToTimeSpan();
            TimeSpan timeSpanInWholeMinutes = new TimeSpan(timeSpanOriginal.Hours, timeSpanOriginal.Minutes, 0);

            return new DateTimeOffset(
                zoned.Year,
                zoned.Month,
                zoned.Day,
                zoned.Hour,
                zoned.Minute,
                zoned.Second,
                zoned.Millisecond,
                timeSpanInWholeMinutes
            );

            //return zoned.ToDateTimeOffset();
        }
    }
}