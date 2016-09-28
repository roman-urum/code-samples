using System;
using System.Collections.Generic;

namespace HealthLibrary.Web.Api.Models.Programs
{
    /// <summary>
    /// Represents program info for calendar.
    /// </summary>
    public class ProgramScheduleDto
    {
        /// <summary>
        /// Gets or sets the program identifier.
        /// </summary>
        /// <value>
        /// The program identifier.
        /// </value>
        public Guid ProgramId { get; set; }

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

        /// <summary>
        /// Gets or sets the name of the program.
        /// </summary>
        /// <value>
        /// The name of the program.
        /// </value>
        public string ProgramName { get; set; }

        /// <summary>
        /// List of calendar events for this program.
        /// </summary>
        public IList<ProgramScheduleEventDto> ProgramEvents { get; set; }
    }
}