using System;

namespace Maestro.Domain.Dtos.PatientsService.Calendar
{
    /// <summary>
    /// CalendarResponseProgramDto.
    /// </summary>
    public class CalendarProgramResponseDto : BaseCalendarProgramDto
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