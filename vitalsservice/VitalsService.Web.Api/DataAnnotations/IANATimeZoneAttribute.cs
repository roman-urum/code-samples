using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace VitalsService.Web.Api.DataAnnotations
{
    /// <summary>
    /// IANATimeZoneAttribute.
    /// </summary>
    public class IANATimeZoneAttribute : ValidationAttribute
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        /// true if the specified value is valid; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            var timeZoneId = value as string;

            return timeZoneId != null && DateTimeZoneProviders.Tzdb.Ids.Contains(timeZoneId);
        }
    }
}