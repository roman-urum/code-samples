using System.Collections.Generic;
using HealthLibrary.Web.Api.Models.Elements.SelectionAnswerChoices;

namespace HealthLibrary.Web.Api.Models.Elements.SelectionAnswerSets
{
    /// <summary>
    /// Model for response with answer-set data.
    /// </summary>
    public class SelectionAnswerSetResponseDto : OpenEndedAnswerSetResponseDto
    {
        /// <summary>
        /// Possible answers for current answerset.
        /// </summary>
        public IEnumerable<SelectionAnswerChoiceResponseDto> SelectionAnswerChoices { get; set; }

        /// <summary>
        /// Type of answer selection.
        /// </summary>
        public bool IsMultipleChoice { get; set; }
    }
}