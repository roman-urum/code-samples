using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.PatientsService.Calendar
{
    /// <summary>
    /// CreatedCalendarItemRequestDto.
    /// </summary>
    [JsonObject]
    public class CalendarItemRequestDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the program day.
        /// </summary>
        /// <value>
        /// The program day.
        /// </value>
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
        /// Gets or sets the expire minutes.
        /// </summary>
        /// <value>
        /// The expire minutes.
        /// </value>
        public int? ExpireMinutes { get; set; }

        /// <summary>
        /// Gets or sets the protocols.
        /// </summary>
        /// <value>
        /// The protocols.
        /// </value>
        public IList<ProtocolElementDto> Protocols { get; set; }

        /// <summary>
        /// Gets or sets the reccurence rules (for reccuring calendar items only).
        /// </summary>
        /// <value>
        /// The reccurence rules.
        /// </value>
        public IList<RecurrenceRuleDto> RecurrenceRules { get; set; }
    }
}
