using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maestro.Domain.Dtos.PatientsService.Calendar
{
    /// <summary>
    /// Class ClearCalendarRequestDto.
    /// </summary>
    [JsonObject]
    public class ClearCalendarRequestDto:TerminateProgramRequest
    {
        /// <summary>
        /// Gets or sets the scheduled after.
        /// </summary>
        /// <value>The scheduled after.</value>
        public DateTime ScheduledAfter { get; set; }
    }
}
