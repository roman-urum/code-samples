using System.Collections.Generic;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.PatientsService.Calendar
{
    /// <summary>
    /// CreateCalendarProgramRequestDto.
    /// </summary>
    [JsonObject]
    public class CreateCalendarProgramRequestDto : BaseCalendarProgramDto
    {
        /// <summary>
        /// List of calendar events for this program.
        /// </summary>
        public IList<CalendarItemRequestDto> ProgramEvents { get; set; }
    }
}