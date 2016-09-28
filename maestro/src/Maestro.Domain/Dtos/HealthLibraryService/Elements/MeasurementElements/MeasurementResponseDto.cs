using Maestro.Domain.Dtos.HealthLibraryService.Enums;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.MeasurementElements
{
    /// <summary>
    /// MeasurementResponseDto.
    /// </summary>
    public class MeasurementResponseDto : ElementDto
    {
        /// <summary>
        /// Gets or sets the type of the measurement.
        /// </summary>
        /// <value>
        /// The type of the media.
        /// </value>
        public MeasurementType MeasurementType { get; set; }

        /// <summary>
        /// Gets or sets the name of measurement.
        /// </summary>
        public string Name { get; set; }
    }
}