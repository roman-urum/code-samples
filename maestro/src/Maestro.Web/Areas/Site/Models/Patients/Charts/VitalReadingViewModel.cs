namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// VitalReadingViewModel.
    /// </summary>
    public class VitalReadingViewModel : BaseReadingViewModel
    {
        /// <summary>
        /// Gets or sets the measurement.
        /// </summary>
        /// <value>
        /// The measurement.
        /// </value>
        public MeasurementBriefViewModel Measurement { get; set; }

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
        /// Gets or sets the indicator whether the vital is automated or not.
        /// </summary>
        /// <value>
        /// The indicator whether the vital is automated or not.
        /// </value>
        public bool IsAutomated { get; set; }
    }
}