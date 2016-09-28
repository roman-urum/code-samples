using System.ComponentModel.DataAnnotations;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Protocols
{
    /// <summary>
    /// UpdateProtocolLocalizedNameRequestDto.
    /// </summary>
    public class UpdateProtocolLocalizedNameRequestDto
    {
        /// <summary>
        /// Gets or sets the name of the localized.
        /// </summary>
        /// <value>
        /// The name of the localized.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [StringLength(
            50,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string LocalizedName { get; set; }
    }
}