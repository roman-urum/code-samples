using System;

namespace Maestro.Web.Areas.Site.Models.Patients.DetailedData
{
    /// <summary>
    /// SearchUngroupedHealthSessionDetailedDataViewModel.
    /// </summary>
    public class SearchUngroupedHealthSessionDetailedDataViewModel : SearchAdhocMeasurementsDetailedDataViewModel
    {
        /// <summary>
        /// Gets or sets the question identifier.
        /// </summary>
        /// <value>
        /// The question identifier.
        /// </value>
        public Guid? QuestionId { get; set; }
    }
}