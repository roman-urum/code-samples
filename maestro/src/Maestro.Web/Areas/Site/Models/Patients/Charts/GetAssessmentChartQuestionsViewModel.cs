using System;

namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// GetHealthSessionQuestionsViewModel.
    /// </summary>
    public class GetAssessmentChartQuestionsViewModel
    {
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }
    }
}