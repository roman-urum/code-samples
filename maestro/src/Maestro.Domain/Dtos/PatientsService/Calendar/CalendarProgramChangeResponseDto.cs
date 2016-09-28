using System;
using Maestro.Domain.Dtos.PatientsService.Enums;

namespace Maestro.Domain.Dtos.PatientsService.Calendar
{
    /// <summary>
    /// Model for response with details of changes in calendar program.
    /// </summary>
    public class CalendarProgramChangeResponseDto : CalendarChangeResponseDto
    {
        /// <summary>
        /// New value of date when event will be started.
        /// </summary>
        public DateTime StartDateUtc { get; set; }

        /// <summary>
        /// Action which was performed.
        /// </summary>
        public CalendarProgramAction Action { get; set; }

        /// <summary>
        /// Number of day in program from which program will be started.
        /// </summary>
        public int StartDay { get; set; }

        /// <summary>
        /// Number of day in program on which program will be ended.
        /// </summary>
        public int EndDay { get; set; }

        /// <summary>
        /// Date from which program will be/was terminated.a
        /// </summary>
        public DateTime? TerminationUtc { get; set; }
    }
}
