using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.PatientsService.Calendar
{
    /// <summary>
    /// Model for request to create set of new calendar items.
    /// </summary>
    [JsonObject]
    public class CreateCalendarItemsRequestDto
    {
        /// <summary>
        /// List of calendar events for this program.
        /// </summary>
        public IList<CalendarItemRequestDto> CalendarEvents { get; set; }
    }
}
