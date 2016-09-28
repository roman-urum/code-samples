using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Domain.Dtos.VitalsService
{
    /// <summary>
    /// HealthSessionResponseDto.
    /// </summary>
    public class HealthSessionResponseDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// GUID of the protocol being taken.
        /// </summary>
        public Guid? ProtocolId { get; set; }

        /// <summary>
        /// Name of the protocol being taken
        /// </summary>
        public string ProtocolName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is private.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is private; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is incomplete.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is incomplete; otherwise, <c>false</c>.
        /// </value>
        public bool IsIncomplete { get; set; }

        /// <summary>
        /// Debugging, Testing or Production
        /// </summary>
        public ProcessingType ProcessingType { get; set; }

        /// <summary>
        /// The UTC date/time this health session was scheduled for.
        /// </summary>
        public DateTime ScheduledUtc { get; set; }

        /// <summary>
        /// Gets or sets the scheduled timezone.
        /// </summary>
        /// <value>
        /// The scheduled timezone.
        /// </value>
        public string ScheduledTz { get; set; }

        /// <summary>
        /// The calendar item ID that triggered this health session
        /// </summary>
        public Guid? CalendarItemId { get; set; }

        /// <summary>
        /// The UTC date/time this health session was started by patient
        /// </summary>
        public DateTime StartedUtc { get; set; }

        /// <summary>
        /// Gets or sets the started timezone.
        /// </summary>
        /// <value>
        /// The started timezone.
        /// </value>
        public string StartedTz { get; set; }

        /// <summary>
        /// The UTC date/time this health session was completed by patient
        /// </summary>
        public DateTime CompletedUtc { get; set; }

        /// <summary>
        /// Gets or sets the completed timezone.
        /// </summary>
        /// <value>
        /// The completed timezone.
        /// </value>
        public string CompletedTz { get; set; }

        /// <summary>
        /// Utc date and time when health session was submitted.
        /// </summary>
        public DateTime SubmittedUtc { get; set; }

        /// <summary>
        /// The actual elements presented to the patient.
        /// </summary>
        public IList<HealthSessionElementDto> Elements { get; set; }
    }
}
