using System.ComponentModel.DataAnnotations;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models.PatientNotes
{
    /// <summary>
    /// SuggestedNotableRequestDto.
    /// </summary>
    public class SuggestedNotableRequestDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [StringLength(
            50, 
            ErrorMessageResourceType = typeof(GlobalStrings), 
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError"
        )]
        public string Name { get; set; }
    }
}