using System.Collections.Generic;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerChoices;
using Maestro.Web.DataAnnotations;
using Maestro.Web.Resources;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerSet
{
    /// <summary>
    /// CreateSelectionAnswerSetDto.
    /// </summary>
    public class CreateSelectionAnswerSetViewModel : BaseSelectionAnswerSetViewModel
    {
        /// <summary>
        /// Possible answers for current answerset.
        /// </summary>
        [NotEmptyList(ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "NotEmptyList_ValidationError", ErrorMessage = null)]
        public IEnumerable<CreateSelectionAnswerChoiceViewModel> SelectionAnswerChoices { get; set; }
    }
}