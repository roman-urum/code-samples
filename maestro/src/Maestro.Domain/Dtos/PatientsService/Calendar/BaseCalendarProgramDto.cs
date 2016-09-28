using System;

namespace Maestro.Domain.Dtos.PatientsService.Calendar
{
    /// <summary>
    /// BaseCalendarProgramDto.
    /// </summary>
    /// <seealso cref="Maestro.Domain.Dtos.PatientsService.Calendar.CalendarProgramDto" />
    public class BaseCalendarProgramDto : CalendarProgramDto
    {
        /// <summary>
        /// Gets or sets the program identifier.
        /// </summary>
        /// <value>
        /// The program identifier.
        /// </value>
        public Guid ProgramId { get; set; }

        /// <summary>
        /// Gets or sets the name of the program.
        /// </summary>
        /// <value>
        /// The name of the program.
        /// </value>
        public string ProgramName { get; set; }

        /// <summary>
        /// Gets or sets the start date UTC.
        /// </summary>
        /// <value>
        /// The start date UTC.
        /// </value>
        public DateTime StartDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the start day.
        /// </summary>
        /// <value>
        /// The start day.
        /// </value>
        public int StartDay { get; set; }

        /// <summary>
        /// Gets or sets the end day.
        /// </summary>
        /// <value>
        /// The end day.
        /// </value>
        public int EndDay { get; set; }

        /// <summary>
        /// The number of minutes after which the calendar event should expire. 
        /// Optional. Default is null.
        /// </summary>
        public int? ExpireMinutes { get; set; }
    }
}
