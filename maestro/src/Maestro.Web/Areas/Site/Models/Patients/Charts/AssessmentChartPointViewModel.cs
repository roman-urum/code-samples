using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// AssessmentChartPointViewModel.
    /// </summary>
    public class AssessmentChartPointViewModel : BaseChartPointViewModel
    {
        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public List<BaseAnswerStatisticViewModel> Values { get; set; }
    }
}