using System.ComponentModel.DataAnnotations;
using Maestro.Web.Resources;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.LocalizedStrings
{
    /// <summary>
    /// Model for localized string.
    /// </summary>
    public class BaseLocalizedStringViewModel
    {
        /// <summary>
        /// Translate.
        /// </summary>
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [MaxLength(
            500,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Value { get; set; }

        /// <summary>
        /// Pronunciation of localized string.
        /// </summary>
        public string Pronunciation { get; set; }
    }
}