using System.ComponentModel.DataAnnotations;
using System.Linq;
using HealthLibrary.Common;

namespace HealthLibrary.Web.Api.DataAnnotations
{
    /// <summary>
    /// SupportedContentTypeAttribute.
    /// </summary>
    public class SupportedContentTypeAttribute : ValidationAttribute
    {
        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            
            return Settings.SupportedExtensionsAndMimeTypes.Any(e => e.Value.Contains((string)value));
        }
    }
}