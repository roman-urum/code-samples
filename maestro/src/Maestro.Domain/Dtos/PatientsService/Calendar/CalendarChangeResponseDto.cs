using System;
using System.Runtime.Serialization;
using Maestro.Domain.Dtos.PatientsService.DefaultSessions;
using Maestro.Domain.Dtos.PatientsService.Enums;

namespace Maestro.Domain.Dtos.PatientsService.Calendar
{
    /// <summary>
    /// Contains base info about changes in calendar entity.
    /// </summary>
    [KnownType(typeof(CalendarItemChangeResponseDto))]
    [KnownType(typeof(CalendarProgramChangeResponseDto))]
    [KnownType(typeof(DefaultSessionChangeResponseDto))]
    public class CalendarChangeResponseDto
    {
        /// <summary>
        /// Name of user who made changes.
        /// </summary>
        public string ChangedBy { get; set; }

        /// <summary>
        /// Date and time when changes has been applied.
        /// </summary>
        public DateTime ChangedUtc { get; set; }

        /// <summary>
        /// Name of element which was changed.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of calendar element.
        /// </summary>
        public CalendarElementType ElementType { get; set; }
    }
}