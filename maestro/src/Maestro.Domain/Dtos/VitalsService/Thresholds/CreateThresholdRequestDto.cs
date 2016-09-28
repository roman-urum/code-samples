using System;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.VitalsService.Thresholds
{
    /// <summary>
    /// CreateThresholdRequestDto.
    /// </summary>
    [JsonObject]
    public class CreateThresholdRequestDto : BaseThresholdRequestDto
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