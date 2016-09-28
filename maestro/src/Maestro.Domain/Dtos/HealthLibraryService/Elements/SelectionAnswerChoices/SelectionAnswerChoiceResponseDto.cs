using System;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerChoices
{
    /// <summary>
    /// Model of answer from answer set.
    /// </summary>
    public class SelectionAnswerChoiceResponseDto : BaseSelectionAnswerChoiceDto
    {
        /// <summary>
        /// Id of answer choice.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Localized answer strings.
        /// </summary>
        public LocalizedStringWithAudioFileMediaResponseDto AnswerString { get; set; }
    }
}