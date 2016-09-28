using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using HealthLibrary.Common;

namespace HealthLibrary.Web.Api.DataAnnotations
{
    /// <summary>
    /// FileNameAttribute.
    /// </summary>
    public class FileNameAttribute : ValidationAttribute
    {
        private readonly List<string> mediaExtensions = Settings.SupportedExtensionsAndMimeTypes.Keys.ToList();

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null || mediaExtensions.Any(mExt => value.ToString().EndsWith(mExt, true, CultureInfo.InvariantCulture)))
            {
                return true;
            }

            return false;
        }
    }
}