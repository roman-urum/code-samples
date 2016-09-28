namespace Maestro.Web.Areas.Site.Models.Patients.Dashboard
{
    /// <summary>
    /// PatientPeripheralViewModel.
    /// </summary>
    public class PatientPeripheralViewModel
    {
        /// <summary>
        /// Gets or sets the name of the peripheral.
        /// </summary>
        /// <value>
        /// The name of the peripheral.
        /// </value>
        public string PeripheralName { get; set; }

        // ToDo: Fields below will be added in future scope
        ///// <summary>
        ///// Gets or sets the most recent measurement battery percent.
        ///// </summary>
        ///// <value>
        ///// The most recent measurement battery percent.
        ///// </value>
        //public decimal? MostRecentMeasurementBatteryPercent { get; set; }

        ///// <summary>
        ///// Gets or sets the most recent measurement date.
        ///// </summary>
        ///// <value>
        ///// The most recent measurement date.
        ///// </value>
        //public DateTime? MostRecentMeasurementDateUtc { get; set; }
    }
}