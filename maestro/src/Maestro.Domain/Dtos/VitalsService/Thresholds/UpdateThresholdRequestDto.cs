using System;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.VitalsService.Thresholds
{
    /// <summary>
    /// UpdateThresholdRequestDto.
    /// </summary>
    [JsonObject]
    public class UpdateThresholdRequestDto : CreateThresholdRequestDto
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