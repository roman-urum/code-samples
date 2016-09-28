using System;
using System.ComponentModel.DataAnnotations;
using DeviceService.Domain.Entities.Enums;

namespace DeviceService.Web.Api.DataAnnotations
{
    /// <summary>
    /// DeviceIdTypeValidationAttribute.
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class DeviceIdTypeValidationAttribute : ValidationAttribute
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
            return value == null || Enum.IsDefined(typeof(DeviceIdType), value);
        }
    }
}