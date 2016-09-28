using System;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.VitalsService.Thresholds
{
    /// <summary>
    /// UpdateDefaultThresholdRequestDto.
    /// </summary>
    [JsonObject]
    public class UpdateDefaultThresholdRequestDto : CreateDefaultThresholdRequestDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }
    }
}