using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using HealthLibrary.Web.Api.Models.Elements.Medias;

namespace HealthLibrary.Web.Api.DataAnnotations
{
    /// <summary>
    /// Validates that extension of file provided in model matches to specified extensions.
    /// </summary>
    public class MediaExtensionAttribute : ValidationAttribute
    {
        private string[] extensions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaExtensionAttribute"/> class.
        /// </summary>
        /// <param name="extensions">The extensions.</param>
        public MediaExtensionAttribute(params string[] extensions)
        {
            this.extensions = extensions;
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        /// true if the specified value is valid; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            var createMediaRequest = value as CreateMediaRequestDto;

            if (createMediaRequest == null || this.extensions == null || !this.extensions.Any())
            {
                return true;
            }

            if (!string.IsNullOrEmpty(createMediaRequest.SourceContentUrl))
            {
                return this.extensions.Any(
                    ext => createMediaRequest.SourceContentUrl.EndsWith(ext, true, CultureInfo.InvariantCulture));
            }

            if (!string.IsNullOrEmpty(createMediaRequest.OriginalFileName))
            {
                return this.extensions.Any(
                    ext => createMediaRequest.OriginalFileName.EndsWith(ext, true, CultureInfo.InvariantCulture));
            }

            return true;
        }
    }
}