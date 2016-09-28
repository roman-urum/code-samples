using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// VitalsChartViewModel.
    /// </summary>
    public class VitalsChartViewModel : BaseChartViewModel
    {

        /// <summary>
        /// Gets or sets the thresholds.
        /// </summary>
        /// <value>
        /// The thresholds.
        /// </value>
        public List<ThresholdViewModel> Thresholds { get; set; }
    }
}