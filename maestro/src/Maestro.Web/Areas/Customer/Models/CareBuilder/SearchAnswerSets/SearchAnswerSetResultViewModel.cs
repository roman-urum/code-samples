using System.Collections.Generic;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerSet;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.SearchAnswerSets
{
    public class SearchAnswerSetResultDto
    {
        public IList<SearchScaleAnswerSetResultViewModel> ScaleAnswerSets { get; set; }

        public IList<UpdateSelectionAnswerSetViewModel> SelectionAnswerSets { get; set; }
    }
}
