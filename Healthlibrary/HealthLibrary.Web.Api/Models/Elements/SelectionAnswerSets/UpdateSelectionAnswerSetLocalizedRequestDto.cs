using System.Collections.Generic;
using HealthLibrary.Web.Api.Models.Elements.SelectionAnswerChoices;

namespace HealthLibrary.Web.Api.Models.Elements.SelectionAnswerSets
{
    /// <summary>
    /// Model to provide translations for answerchoices in answerset.
    /// </summary>
    public class UpdateSelectionAnswerSetLocalizedRequestDto
    {
        /// <summary>
        /// Localized strings for selection answer choices.
        /// </summary>
        public IEnumerable<UpdateSelectionAnswerChoiceLocalizedRequestDto> SelectionAnswerChoices { get; set; }
    }
}