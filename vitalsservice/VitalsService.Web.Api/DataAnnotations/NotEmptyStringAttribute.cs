using System.ComponentModel.DataAnnotations;

namespace VitalsService.Web.Api.DataAnnotations
{
    /// <summary>
    /// NotEmptyStringAttribute.
    /// </summary>
    public class NotEmptyStringAttribute : ValidationAttribute
    {
        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            return value == null || value.ToString().Length > 0;
        }
    }
}