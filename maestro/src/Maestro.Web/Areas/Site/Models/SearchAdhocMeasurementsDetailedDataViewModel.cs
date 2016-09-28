using Maestro.Domain.Dtos.VitalsService.Enums;
using Maestro.Web.Areas.Site.Models.Patients.DetailedData;

namespace Maestro.Web.Areas.Site.Models
{
    /// <summary>
    /// SearchAdhocMeasurementsDetailedDataViewModel.
    /// </summary>
    public class SearchAdhocMeasurementsDetailedDataViewModel : SearchDetailedDataViewModel
    {
        /// <summary>
        /// Gets or sets the type of the vital.
        /// </summary>
        /// <value>
        /// The type of the vital.
        /// </value>
        public VitalType? VitalType { get; set; }
    }
}