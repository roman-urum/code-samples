using System;
using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients.Dashboard
{
    /// <summary>
    /// LatestInformationDashboardViewModel.
    /// </summary>
    public class PatientLatestInformationDashboardViewModel
    {
        /// <summary>
        /// Gets or sets the latest health session date.
        /// </summary>
        /// <value>
        /// The latest health session date.
        /// </value>
        public string LatestHealthSessionDate { get; set; }

        /// <summary>
        /// Gets or sets the latest readings.
        /// </summary>
        /// <value>
        /// The latest readings.
        /// </value>
        public IList<VitalReadingWithTrendViewModel> LatestReadings { get; set; }

        /// <summary>
        /// Gets or sets the latest health session readings.
        /// </summary>
        /// <value>
        /// The latest health session readings.
        /// </value>
        public IList<VitalReadingWithTrendViewModel> LatestHealthSessionReadings { get; set; }

        /// <summary>
        /// Gets or sets the latest health session questions and answers.
        /// </summary>
        /// <value>
        /// The latest health session questions and answers.
        /// </value>
        public IList<QuestionReadingViewModel> LatestHealthSessionQuestionsAndAnswers { get; set; }
    }
}