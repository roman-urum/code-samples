using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos.PatientsService.Enums;

namespace Maestro.Domain.Dtos.PatientsService.Calendar
{
    /// <summary>
    /// Contains data of response with adherence.
    /// </summary>
    public class AdherenceDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the scheduled UTC.
        /// </summary>
        /// <value>
        /// The scheduled UTC.
        /// </value>
        public DateTime ScheduledUtc { get; set; }

        /// <summary>
        /// Gets or sets the expiration UTC.
        /// </summary>
        /// <value>
        /// The expiration UTC.
        /// </value>
        public DateTime? ExpirationUtc { get; set; }

        /// <summary>
        /// Gets or sets the event timezone.
        /// </summary>
        /// <value>
        /// The event timezone.
        /// </value>
        public string EventTz { get; set; }

        /// <summary>
        /// Gets or sets the updated UTC.
        /// </summary>
        /// <value>
        /// The updated UTC.
        /// </value>
        public DateTime UpdatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public AdherenceStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the health session identifiers.
        /// </summary>
        /// <value>
        /// The health session identifiers.
        /// </value>
        public List<Guid> HealthSessionIds { get; set; }

        /// <summary>
        /// Indicating whether entity deleted or not.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the calendar event.
        /// </summary>
        /// <value>
        /// The calendar event.
        /// </value>
        public CalendarItemResponseDto CalendarEvent { get; set; }
    }
}
