using Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.QuestionElements
{
    /// <summary>
    /// Dto to transfer question element data.
    /// </summary>
    [JsonObject]
    public class CreateQuestionElementRequestDto : BaseQuestionElementRequestDto
    {
        /// <summary>
        /// Localized string with question and audio prompt.
        /// </summary>
        public CreateLocalizedStringRequestDto QuestionElementString { get; set; }
    }
}