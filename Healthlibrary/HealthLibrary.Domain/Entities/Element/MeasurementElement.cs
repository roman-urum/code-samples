using HealthLibrary.Domain.Entities.Enums;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// MeasurementElement.
    /// </summary>
    public class MeasurementElement : Element
    {
        /// <summary>
        /// Gets or sets the type of the media.
        /// </summary>
        /// <value>
        /// The type of the media.
        /// </value>
        public MeasurementType MeasurementType { get; set; }

        /// <summary>
        /// Name of measurement element.
        /// </summary>
        public string Name { get; set; }
    }
}