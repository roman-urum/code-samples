using System;
using System.ComponentModel.DataAnnotations;

namespace VitalsService.Web.Api.DataAnnotations
{
    /// <summary>
    /// ForbidDefaultNotNullableValueAttribute.
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class ForbidDefaultNotNullableValueAttribute : ValidationAttribute
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
            if (value == null)
            {
                return true;
            }

            var type = value.GetType();

            if (!type.IsValueType)
            {
                return true;
            }

            var defaultValue = Activator.CreateInstance(type);

            return !value.Equals(defaultValue);
        }
    }
}