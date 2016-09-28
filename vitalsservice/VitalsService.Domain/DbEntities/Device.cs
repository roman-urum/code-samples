using System;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// Device.
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Gets or sets the measurement identifier.
        /// </summary>
        /// <value>
        /// The measurement identifier.
        /// </value>
        public Guid MeasurementId { get; set; }
                    
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public string UniqueIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the battery percent.
        /// </summary>
        /// <value>
        /// The battery percent.
        /// </value>
        public decimal? BatteryPercent { get; set; }

        /// <summary>
        /// Gets or sets the battery millivolts.
        /// </summary>
        /// <value>
        /// The battery millivolts.
        /// </value>
        public int? BatteryMillivolts { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the measurement.
        /// </summary>
        /// <value>
        /// The measurement.
        /// </value>
        public virtual Measurement Measurement { get; set; }
    }
}