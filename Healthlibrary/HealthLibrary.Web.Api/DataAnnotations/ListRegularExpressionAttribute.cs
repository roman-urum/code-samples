using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace HealthLibrary.Web.Api.DataAnnotations
{
    /// <summary>
    /// Validates that all string in list are match to provided expression.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ListRegularExpressionAttribute : ValidationAttribute
    {
        private readonly Regex regex;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListRegularExpressionAttribute"/> class.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        public ListRegularExpressionAttribute(string pattern)
        {
            this.regex = new Regex(pattern);
        }

        /// <summary>
        /// Determines whether the specified value of the object is valid. 
        /// </summary>
        /// <returns>
        /// true if the specified value is valid; otherwise, false.
        /// </returns>
        /// <param name="value">The value of the object to validate. </param>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var inputList = value as List<string>;

            if (inputList != null && !inputList.All(s => regex.IsMatch(s) && !string.IsNullOrEmpty(s)))
            {
                return false;
            }

            return true;
        }
    }
}