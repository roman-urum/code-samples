using System;
using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// Model for settings of vital chart.
    /// </summary>
    public class VitalChartSettingViewModel : ChartSettingViewModel
    {
        /// <summary>
        /// Name of vital.
        /// </summary>
        public string Name { get; set; }

        public bool ShowMin { get; set; }

        public bool ShowMax { get; set; }

        public bool ShowAverage { get; set; }

        public IList<Guid> ShowThresholdIds { get; set; }
    }
}