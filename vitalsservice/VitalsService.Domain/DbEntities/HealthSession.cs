using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VitalsService.Domain.Constants;
using VitalsService.Domain.Enums;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// Db entity for health session data.
    /// </summary>
    public class HealthSession : Entity
    {
        /// <summary>
        /// Id of customer assigned to patient.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Id of patient.
        /// </summary>
        public Guid PatientId { get; set; }

        /// <summary>
        /// GUID of the protocol being taken.
        /// </summary>
        public Guid? ProtocolId { get; set; }

        /// <summary>
        /// Name of the protocol being taken
        /// </summary>
        [MaxLength(DbConstraints.MaxLength.ProtocolName)]
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
        /// the UTC date/time this health session was scheduled for.
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
        /// the calendar item ID that triggered this health session
        /// </summary>
        public Guid? CalendarItemId { get; set; }

        /// <summary>
        /// the UTC date/time this health session was started by patient
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
        /// the UTC date/time this health session was completed by patient
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
        /// the UTC date/time this health session was received by Maestro
        /// if client supplies this value, it is ignored and set server side on POST 
        /// </summary>
        public DateTime SubmittedUtc { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public string ClientId { get; set; }

        /// <summary>
        /// the actual elements presented to the patient
        /// </summary>
        public virtual ICollection<HealthSessionElement> Elements { get; set; }
    }
}