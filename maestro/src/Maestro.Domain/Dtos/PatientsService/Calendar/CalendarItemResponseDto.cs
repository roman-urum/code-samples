using System;
using System.Collections.Generic;

namespace Maestro.Domain.Dtos.PatientsService.Calendar
{
    /// <summary>
    /// Dto with info about calendar event.
    /// </summary>
    public class CalendarItemResponseDto
    {
        /// <summary>
        /// Id of calendar event.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id of related program.
        /// </summary>
        public Guid? CalendarProgramId { get; set; }

        /// <summary>
        /// Id of program from health library.
        /// </summary>
        public Guid? ProgramId { get; set; }

        /// <summary>
        /// Program name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Number of day in program.
        /// </summary>
        public int? ProgramDay { get; set; }

        /// <summary>
        /// Gets or sets the due UTC.
        /// </summary>
        /// <value>
        /// The due UTC.
        /// </value>
        public DateTime? DueUtc { get; set; }

        /// <summary>
        /// Gets or sets the event timezone.
        /// </summary>
        /// <value>
        /// The event timezone.
        /// </value>
        public string EventTz { get; set; }

        /// <summary>
        /// Key to combine events from the same program.
        /// </summary>
        public Guid? Correlator { get; set; }

        /// <summary>
        /// Gets or sets the expire minutes.
        /// </summary>
        /// <value>
        /// The expire minutes.
        /// </value>
        public int? ExpireMinutes { get; set; }

        /// <summary>
        /// Gets or sets the list of protocol elements.
        /// </summary>
        /// <value>
        /// The list of protocol elements.
        /// </value>
        public List<ProtocolElementDto> Protocols { get; set; }

        /// <summary>
        /// Gets or sets the list of recurrence rules.
        /// </summary>
        /// <value>
        /// The list of recurrence rules.
        /// </value>
        public List<RecurrenceRuleDto> RecurrenceRules { get; set; }
    }
}