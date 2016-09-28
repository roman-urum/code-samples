using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Maestro.Web.DataAnnotations
{
    /// <summary>
    /// Attribute to check that list of elements is not empty.
    /// </summary>
    public class NotEmptyListAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;

            return list != null && list.Count > 0;
        }
    }
}