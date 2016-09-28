using Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerChoices
{
    /// <summary>
    /// Model of answer from answer set.
    /// </summary>
    [JsonObject]
    public class CreateSelectionAnswerChoiceRequestDto : BaseSelectionAnswerChoiceDto
    {
        /// <summary>
        /// Default string for answer choice.
        /// </summary>
        public CreateLocalizedStringRequestDto AnswerString { get; set; }
    }
}