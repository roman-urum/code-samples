using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VitalsService.Web.Api.DataAnnotations
{
    /// <summary>
    /// Attribute to validate that collection contains unique list of elements.
    /// </summary>
    public class UniqueListAttribute : ValidationAttribute
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
            var list = value as IList;

            if (list == null)
            {
                return true;
            }

            var distinctList = new List<object>();

            foreach (var element in list)
            {
                if (!distinctList.Contains(element))
                {
                    distinctList.Add(element);
                }
            }

            return distinctList.Count == list.Count;
        }
    }
}