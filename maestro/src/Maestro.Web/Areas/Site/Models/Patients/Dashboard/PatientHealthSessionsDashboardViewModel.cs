using System;
using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients.Dashboard
{
    /// <summary>
    /// PatientHealthSessionsDashboardViewModel.
    /// </summary>
    public class PatientHealthSessionsDashboardViewModel
    {
        /// <summary>
        /// Gets or sets the next health session date UTC.
        /// </summary>
        /// <value>
        /// The next health session date UTC.
        /// </value>
        public DateTimeOffset? NextHealthSessionDate { get; set; }

        /// <summary>
        /// Gets or sets the active programs.
        /// </summary>
        /// <value>
        /// The active programs.
        /// </value>
        public IList<string> ActivePrograms { get; set; }

        /// <summary>
        /// Gets or sets the health sessions missed count.
        /// </summary>
        /// <value>
        /// The health sessions missed count.
        /// </value>
        public int HealthSessionsMissedCount { get; set; }

        /// <summary>
        /// Gets or sets the last type of the connected device.
        /// </summary>
        /// <value>
        /// The last type of the connected device.
        /// </value>
        public string LastConnectedDeviceType { get; set; }

        /// <summary>
        /// Gets or sets the last connected date.
        /// </summary>
        /// <value>
        /// The last connected date.
        /// </value>
        public DateTimeOffset? LastConnectedDate { get; set; }
    }
}