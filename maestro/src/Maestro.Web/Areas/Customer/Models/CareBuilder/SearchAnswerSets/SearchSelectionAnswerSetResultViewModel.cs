using System;
using System.Collections.Generic;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerChoices;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerSet;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.SearchAnswerSets
{
    public class SearchSelectionAnswerSetResultViewModel : BaseSelectionAnswerSetViewModel
    {
        public Guid Id { get; set; }

        public IList<CreateSelectionAnswerChoiceViewModel> SelectionAnswerChoices { get; set; }
    }
}