using System;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// Vital.
    /// </summary>
    public class Vital : Entity
    {
        /// <summary>
        /// Gets or sets the measurement identifier.
        /// </summary>
        /// <value>
        /// The measurement identifier.
        /// </value>
        public Guid MeasurementId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets the measurement.
        /// </summary>
        /// <value>
        /// The measurement.
        /// </value>
        public virtual Measurement Measurement { get; set; }

        /// <summary>
        /// Gets or sets the vital alert details.
        /// </summary>
        /// <value>
        /// The vital alert details.
        /// </value>
        public virtual VitalAlert VitalAlert { get; set; }

        // ToDo: Potentially will be added according to the requirements
        //public virtual InsightAlert InsightAlert { get; set; }
    }
}