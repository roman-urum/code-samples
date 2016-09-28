using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using HealthLibrary.Common;

namespace HealthLibrary.Web.Api.DataAnnotations
{
    /// <summary>
    /// MediaUrlAttribute.
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class MediaUrlAttribute : ValidationAttribute
    {
        private readonly List<string> mediaExtensions = Settings.SupportedExtensionsAndMimeTypes.Keys.ToList();

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        /// true if the specified value is valid; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            Uri uriResult;

            bool isValid = Uri.TryCreate(value.ToString(), UriKind.Absolute, out uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isValid)
            {
                return false;
            }

            string strValue = value.ToString();

            int startParamsIndex = strValue.IndexOf('?');

            if (startParamsIndex > 0)
            {
                strValue = strValue.Substring(0, startParamsIndex);
            }

            return mediaExtensions.Any(mExt => strValue.EndsWith(mExt, true, CultureInfo.InvariantCulture));
        }
    }
}