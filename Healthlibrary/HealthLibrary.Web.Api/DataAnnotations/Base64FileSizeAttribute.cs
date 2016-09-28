using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace HealthLibrary.Web.Api.DataAnnotations
{
    /// <summary>
    /// Validates size of file encoded in base 64 string.
    /// </summary>
    public class Base64FileSizeAttribute : ValidationAttribute
    {
        private readonly int maxFileSize;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="maxFileSize">Max allowed size of files in bytes.</param>
        public Base64FileSizeAttribute(int maxFileSize)
        {
            this.maxFileSize = maxFileSize;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var valueString = value.ToString();

            if (string.IsNullOrEmpty(valueString))
            {
                return true;
            }

            try
            {
                var file = Convert.FromBase64String(valueString);

                return file.Length <= this.maxFileSize;
            }
            catch (FormatException)
            {
                return true;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            string errorMessageString = this.ErrorMessageString;
            object[] objArray = new object[2];

            objArray[0] = (object)name;
            objArray[1] = (object)(this.maxFileSize / 1024 / 1024);

            return string.Format((IFormatProvider)currentCulture, errorMessageString, objArray);
        }
    }
}