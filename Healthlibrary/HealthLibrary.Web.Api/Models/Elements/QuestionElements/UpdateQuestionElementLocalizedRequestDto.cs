using System.ComponentModel.DataAnnotations;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;

namespace HealthLibrary.Web.Api.Models.Elements.QuestionElements
{
    /// <summary>
    /// Model to add or update localized strings for question element.
    /// </summary>
    public class UpdateQuestionElementLocalizedRequestDto
    {
        /// <summary>
        /// Localized string with question and audio prompt.
        /// </summary>
        [Required]
        public CreateLocalizedStringRequestDto QuestionElementString { get; set; }
    }
}