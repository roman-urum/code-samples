using System;

namespace Maestro.Domain.Dtos.VitalsService.Thresholds
{
    /// <summary>
    /// ThresholdDto.
    /// </summary>
    public class ThresholdDto : BaseThresholdDto
    {
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId { get; set; }
    }
}