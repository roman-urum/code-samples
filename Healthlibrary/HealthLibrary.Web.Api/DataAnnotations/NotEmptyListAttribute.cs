using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace HealthLibrary.Web.Api.DataAnnotations
{
    /// <summary>
    /// Attribute to check that list of elements is not empty.
    /// </summary>
    public class NotEmptyListAttribute : ValidationAttribute
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
            var list = value as IList;

            return list != null && list.Count > 0;
        }
    }
}