using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HealthLibrary.Web.Api.DataAnnotations
{
    /// <summary>
    /// Attribute to validate that collection contains unique list of strings.
    /// </summary>
    public class UniqueStringsListAttribute : ValidationAttribute
    {
        /// <summary>
        /// Determines whether the specified value of the object is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        /// true if the specified value is valid; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            var list = value as IList<string>;

            return list == null || list.Distinct().Count() == list.Count();
        }
    }
}