using System.Collections.Generic;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerChoices;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerSet
{
    /// <summary>
    /// Model for response with answer-set data.
    /// </summary>
    public class SelectionAnswerSetResponseDto : OpenEndedAnswerSetResponseDto
    {
        /// <summary>
        /// Possible answers for current answerset.
        /// </summary>
        public List<SelectionAnswerChoiceResponseDto> SelectionAnswerChoices { get; set; }

        /// <summary>
        /// Type of answer selection.
        /// </summary>
        public bool IsMultipleChoice { get; set; }
    }
}