using System;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// MeasurementValue.
    /// </summary>
    public class MeasurementValue : HealthSessionElementValue
    {
        /// <summary>
        /// Guid of measurement entity.
        /// </summary>
        public Guid MeasurementId { get; set; }

        /// <summary>
        /// Reference to related measurement.
        /// </summary>
        public virtual Measurement Measurement { get; set; }
    }
}