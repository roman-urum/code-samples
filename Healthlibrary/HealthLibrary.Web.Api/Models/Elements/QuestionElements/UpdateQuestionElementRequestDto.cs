using System.ComponentModel.DataAnnotations;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;

namespace HealthLibrary.Web.Api.Models.Elements.QuestionElements
{
    /// <summary>
    /// Container for data to update question element.
    /// </summary>
    public class UpdateQuestionElementRequestDto : BaseQuestionElementRequestDto
    {
        /// <summary>
        /// Localized string with question and audio prompt.
        /// </summary>
        [Required]
        public UpdateLocalizedStringRequestDto QuestionElementString { get; set; }
    }
}