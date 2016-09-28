using System.Collections.Generic;

using Maestro.Domain.Dtos.VitalsService;

namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// Class for caching measurements.
    /// </summary>
    public class CacheMeasurementsItem
    {
        /// <summary>
        /// Gets or sets the date range.
        /// </summary>
        /// <value>
        /// The date range.
        /// </value>
        public ChartDateRangeViewModel DateRange { get; set; }

        /// <summary>
        /// Gets or sets the list of measurements.
        /// </summary>
        /// <value>
        /// The list of measurements.
        /// </value>
        public List<MeasurementDto> Measurements { get; set; }
    }
}