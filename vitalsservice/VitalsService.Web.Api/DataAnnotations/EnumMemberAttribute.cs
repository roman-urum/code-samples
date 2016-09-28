using System;
using System.ComponentModel.DataAnnotations;

namespace VitalsService.Web.Api.DataAnnotations
{
    /// <summary>
    /// Uses this attribute to check that property value
    /// exists in enum.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EnumMemberAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            Type enumType = value.GetType();
            bool isValid = Enum.IsDefined(enumType, value);

            return isValid;
        }
    }
}