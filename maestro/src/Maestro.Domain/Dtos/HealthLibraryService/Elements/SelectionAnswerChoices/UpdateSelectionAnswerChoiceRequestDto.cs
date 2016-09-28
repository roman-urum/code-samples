using System;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerChoices
{
    /// <summary>
    /// Model of answer from answer set with localized answer string.
    /// </summary>
    [JsonObject]
    public class UpdateSelectionAnswerChoiceRequestDto : BaseSelectionAnswerChoiceDto
    {
        /// <summary>
        /// Id of existed element if it should be updated.
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Localized string for answer choice.
        /// </summary>
        public UpdateLocalizedStringRequestDto AnswerString { get; set; }
    }
}