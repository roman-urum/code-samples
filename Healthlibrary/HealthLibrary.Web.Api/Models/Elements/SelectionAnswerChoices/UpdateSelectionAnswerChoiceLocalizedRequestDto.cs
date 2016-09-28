using System;
using System.ComponentModel.DataAnnotations;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Elements.SelectionAnswerChoices
{
    /// <summary>
    /// Model to provide translations for answerchoices in answerset.
    /// </summary>
    public class UpdateSelectionAnswerChoiceLocalizedRequestDto
    {
        /// <summary>
        /// Id of existed element if it should be updated.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Localized string for answer choice.
        /// </summary>
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public UpdateAnswerStringRequestDto AnswerString { get; set; }
    }
}