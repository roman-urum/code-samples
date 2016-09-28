using Maestro.Web.Areas.Site.Models.Patients;

namespace Maestro.Web.Areas.Site.Models.Dashboard
{
    /// <summary>
    /// AlertSeverityCountViewModel class.
    /// </summary>
    public class AlertSeverityCountViewModel : AlertSeverityViewModel
    {
        /// <summary>
        /// Gets or sets the amount of alert severities
        /// </summary>
        public int Count { get; set; }
    }
}