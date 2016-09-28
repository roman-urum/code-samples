using System;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.PatientsService.Calendar
{
    /// <summary>
    /// TerminateProgramRequest.
    /// </summary>
    [JsonObject]
    public class TerminateProgramRequest
    {
        /// <summary>
        /// Gets or sets the termination UTC.
        /// </summary>
        /// <value>
        /// The termination UTC.
        /// </value>
        public DateTime? TerminationUtc { get; set; }
    }
}