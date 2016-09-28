using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// AssessmentChartViewModel.
    /// </summary>
    public class AssessmentChartViewModel : BaseChartViewModel
    {
        /// <summary>
        /// Gets or sets the answers.
        /// </summary>
        /// <value>
        /// The answers.
        /// </value>
        public List<AnswerInfoViewModel> Answers { get; set; }
    }
}