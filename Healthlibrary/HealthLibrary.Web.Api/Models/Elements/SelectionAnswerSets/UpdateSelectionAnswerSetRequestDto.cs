using System.Collections.Generic;
using HealthLibrary.Web.Api.Models.Elements.SelectionAnswerChoices;

namespace HealthLibrary.Web.Api.Models.Elements.SelectionAnswerSets
{
    /// <summary>
    /// Selection answer set dto to update answer set.
    /// </summary>
    public class UpdateSelectionAnswerSetRequestDto : BaseSelectionAnswerSetDto
    {
        /// <summary>
        /// Possible answers for current answerset.
        /// </summary>
        public IEnumerable<UpdateSelectionAnswerChoiceRequestDto> SelectionAnswerChoices { get; set; }
    }
}