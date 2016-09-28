using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace VitalsService.Web.Api.DataAnnotations
{
    /// <summary>
    /// Uses this attribute to check that property value
    /// exists in enum.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ElementStringLengthAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets the minimum length.
        /// </summary>
        /// <value>
        /// The minimum length.
        /// </value>
        public int MinimumLength
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the maximum length.
        /// </summary>
        /// <value>
        /// The maximum length.
        /// </value>
        public int MaximumLength
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementStringLengthAttribute" /> class.
        /// </summary>
        /// <param name="minimumLength">The minimum length.</param>
        /// <param name="maximumLength">The maximum length.</param>
        public ElementStringLengthAttribute(int minimumLength, int maximumLength)
        {
            this.MinimumLength = minimumLength;
            this.MaximumLength = maximumLength;
        }

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

            var inputList = value as List<string>;

            if (inputList == null || inputList.Any(e => e.Length < MinimumLength || e.Length > MaximumLength))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Formats the error message.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;

            string errorMessageString = this.ErrorMessageString;

            object[] objArray = new object[3];

            objArray[0] = (object)name;
            objArray[1] = (object)this.MinimumLength;
            objArray[2] = (object)this.MaximumLength;

            return string.Format((IFormatProvider)currentCulture, errorMessageString, objArray);
        }
    }
}