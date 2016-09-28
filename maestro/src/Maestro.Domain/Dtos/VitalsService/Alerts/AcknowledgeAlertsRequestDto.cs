using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.VitalsService.Alerts
{
    /// <summary>
    /// AcknowledgeAlertsRequestDto.
    /// </summary>
    [JsonObject]
    public class AcknowledgeAlertsRequestDto
    {
        /// <summary>
        /// Gets or sets the acknowledged by.
        /// </summary>
        /// <value>
        /// The acknowledged by.
        /// </value>
        public Guid? AcknowledgedBy { get; set; }

        /// <summary>
        /// Gets or sets the alert ids.
        /// </summary>
        /// <value>
        /// The alert ids.
        /// </value>
        public IList<Guid> AlertIds { get; set; }
    }
}
