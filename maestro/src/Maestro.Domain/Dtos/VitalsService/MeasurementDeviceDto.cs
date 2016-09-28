namespace Maestro.Domain.Dtos.VitalsService
{
    /// <summary>
    /// MeasurementDeviceDto.
    /// </summary>
    public class MeasurementDeviceDto
    {
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
    }
}