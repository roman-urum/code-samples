using System;
using System.Collections.Generic;
using Maestro.Web.Areas.Site.Models.Patients.Charts;
using Newtonsoft.Json;

namespace Maestro.Web.Areas.Site.Models.Dashboard
{
    /// <summary>
    /// FullPatientCardViewModel.
    /// </summary>
    public class FullPatientCardViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the reading.
        /// </summary>
        /// <value>
        /// The reading.
        /// </value>
        public BaseReadingViewModel Reading { get; set; }

        /// <summary>
        /// Gets or sets the threshold.
        /// </summary>
        /// <value>
        /// The threshold.
        /// </value>
        public ThresholdViewModel Threshold { get; set; }

        /// <summary>
        /// Gets or sets the recent readings.
        /// </summary>
        /// <value>
        /// The recent readings.
        /// </value>
        public IList<BaseReadingViewModel> RecentReadings { get; set; }

        /// <summary>
        /// Gets or sets the total recent readings count.
        /// </summary>
        /// <value>
        /// The total recent readings count.
        /// </value>
        public int TotalRecentReadingsCount { get; set; }
    }
}