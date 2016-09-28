using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DeviceService.Web.Api.DataAnnotations
{
    /// <summary>
    /// DateStringAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateStringAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        public string Format { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var format = new[] { Format };

            DateTime expectedDate;
            if (DateTime.TryParseExact(value.ToString(), format,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out expectedDate))
            {
                return true;
            }

            return false;
        }
    }
}