using System;

namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// Model for settings of question chart.
    /// </summary>
    public class QuestionChartSettingViewModel : ChartSettingViewModel
    {
        /// <summary>
        /// Identifier of related question.
        /// </summary>
        public Guid QuestionId { get; set; }
    }
}