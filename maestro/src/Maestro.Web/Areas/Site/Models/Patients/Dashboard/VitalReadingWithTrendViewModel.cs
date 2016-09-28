using Maestro.Domain.Dtos.VitalsService.Enums;
using Maestro.Web.Areas.Site.Models.Patients.Charts;

namespace Maestro.Web.Areas.Site.Models.Patients.Dashboard
{
    /// <summary>
    /// VitalReadingWithTrendViewModel.
    /// </summary>
    public class VitalReadingWithTrendViewModel : VitalReadingViewModel
    {
        /// <summary>
        /// Gets or sets the trend.
        /// </summary>
        /// <value>
        /// The trend.
        /// </value>
        public TrendType Trend { get; set; }
    }
}