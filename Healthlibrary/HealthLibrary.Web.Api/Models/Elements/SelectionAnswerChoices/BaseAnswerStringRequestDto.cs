using System.ComponentModel.DataAnnotations;
using HealthLibrary.Domain.Constants;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Elements.SelectionAnswerChoices
{
    /// <summary>
    /// BaseAnswerStringRequestDto.
    /// </summary>
    public abstract class BaseAnswerStringRequestDto
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
            75,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Value { get; set; }

        /// <summary>
        /// String description.
        /// </summary>
        [MaxLength(
            DbConstraints.MaxLength.Description,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Description { get; set; }

        /// <summary>
        /// String pronuntiation.
        /// </summary>
        [MaxLength(
            1000,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Pronunciation { get; set; }
    }
}