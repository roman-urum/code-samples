using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using VitalsService.Extensions;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.DataAnnotations
{
    /// <summary>
    /// Attribute to check that collection contains any elements.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ItemsRequiredAttribute : ValidationAttribute
    {
        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            var list = value as IList;

            return list != null && list.Count > 0;
        }

        /// <summary>
        /// Formats the error message.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            return GlobalStrings.ItemsRequired_ValidationError.FormatWith(name);
        }
    }
}