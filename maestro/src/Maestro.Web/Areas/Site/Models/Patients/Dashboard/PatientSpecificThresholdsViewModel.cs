using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maestro.Web.Areas.Site.Models.Patients.Dashboard
{
    /// <summary>
    /// PatientSpecificThresholdsViewModel.
    /// </summary>
    public class PatientSpecificThresholdsViewModel
    {
        /// <summary>
        /// Gets or sets the list of thresholds names.
        /// </summary>
        /// <value>
        /// The list of thresholds names.
        /// </value>
        public IList<string> Thresholds { get; set; }

        /// <summary>
        /// Indicates are there overlaped thresholds or not.
        /// </summary>
        public bool ThresholdsOverlap { get; set; }
    }
}