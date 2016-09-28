using Maestro.Domain.Dtos.HealthLibraryService.Enums;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.Protocols
{
    /// <summary>
    /// MeasurementProtocolElementResponseViewModel.
    /// </summary>
    public class MeasurementProtocolElementResponseViewModel : ElementResponseViewModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the measurement.
        /// </summary>
        /// <value>
        /// The type of the measurement.
        /// </value>
        public MeasurementType MeasurementType { get; set; }
    }
}