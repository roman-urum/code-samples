using System.ComponentModel.DataAnnotations;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;

namespace HealthLibrary.Web.Api.Models.Elements.QuestionElements
{
    /// <summary>
    /// Dto to transfer question element data.
    /// </summary>
    public class CreateQuestionElementRequestDto : BaseQuestionElementRequestDto
    {
        /// <summary>
        /// Localized string with question and audio prompt.
        /// </summary>
        [Required]
        public CreateLocalizedStringRequestDto QuestionElementString { get; set; }
    }
}