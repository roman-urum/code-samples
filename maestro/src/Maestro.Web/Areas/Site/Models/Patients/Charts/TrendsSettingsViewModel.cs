using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// Model to save and retriev trends settings.
    /// </summary>
    public class TrendsSettingsViewModel
    {
        /// <summary>
        /// Range of dates for which settings is applied.
        /// </summary>
        public ChartDateRangeViewModel DateRange { get; set; }

        public int? LastDays { get; set; }

        /// <summary>
        /// Settings of charts.
        /// </summary>
        public IList<ChartSettingViewModel> Charts { get; set; } 
    }
}