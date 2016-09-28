using System.Collections.Generic;
using HealthLibrary.Web.Api.DataAnnotations;
using HealthLibrary.Web.Api.Models.Elements.SelectionAnswerChoices;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Elements.SelectionAnswerSets
{
    /// <summary>
    /// Container for new request data.
    /// </summary>
    public class CreateSelectionAnswerSetRequestDto : BaseSelectionAnswerSetDto
    {
        /// <summary>
        /// Possible answers for current answerset.
        /// </summary>
        [NotEmptyList(
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "SelectionAnswerSet_AnswerChoiceRequired_ErrorMessage", 
            ErrorMessage = null
        )]
        public IList<CreateSelectionAnswerChoiceRequestDto> SelectionAnswerChoices { get; set; }
    }
}