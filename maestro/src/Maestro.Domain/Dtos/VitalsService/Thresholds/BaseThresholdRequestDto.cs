using System;
using Maestro.Domain.Dtos.VitalsService.Enums;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.VitalsService.Thresholds
{
    /// <summary>
    /// BaseThresholdRequestDto.
    /// </summary>
    [JsonObject]
    public class BaseThresholdRequestDto
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ThresholdType Type { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public VitalType Name { get; set; }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public decimal MinValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public decimal MaxValue { get; set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        public UnitType Unit { get; set; }

        /// <summary>
        /// Gets or sets the alert severity.
        /// </summary>
        /// <value>
        /// The alert severity.
        /// </value>
        public Guid? AlertSeverityId { get; set; }
    }
}