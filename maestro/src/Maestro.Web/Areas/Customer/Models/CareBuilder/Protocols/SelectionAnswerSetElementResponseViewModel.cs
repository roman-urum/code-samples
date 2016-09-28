using System.Collections.Generic;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.Protocols
{
    public class SelectionAnswerSetElementResponseViewModel : AnswerSetElementResponseViewModel
    {
        /// <summary>
        /// Possible answers for current answerset.
        /// </summary>
        public IEnumerable<SelectionAnswerChoiceElementResponseViewModel> SelectionAnswerChoices { get; set; }

        /// <summary>
        /// Type of answer selection.
        /// </summary>
        public bool IsMultipleChoice { get; set; }

        /// <summary>
        /// Answer set tags.
        /// </summary>
        public IEnumerable<string> Tags { get; set; }
    }
}