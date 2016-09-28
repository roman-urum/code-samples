using System;
using System.Collections.Generic;

namespace VitalsService.Domain.EsbEntities
{
    /// <summary>
    /// MeasurementMessage.
    /// </summary>
    [Serializable]
    public class MeasurementMessage
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the health session identifier.
        /// </summary>
        /// <value>
        /// The health session identifier.
        /// </value>
        public Guid? HealthSessionId { get; set; }

        /// <summary>
        /// Gets or sets the created UTC.
        /// </summary>
        /// <value>
        /// The created UTC.
        /// </value>
        public DateTime CreatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the updated UTC.
        /// </summary>
        /// <value>
        /// The updated UTC.
        /// </value>
        public DateTime UpdatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the observed UTC.
        /// </summary>
        /// <value>
        /// The observed UTC.
        /// </value>
        public DateTime ObservedUtc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is invalidated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is invalidated; otherwise, <c>false</c>.
        /// </value>
        public bool IsInvalidated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is automated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is automated; otherwise, <c>false</c>.
        /// </value>
        public bool IsAutomated { get; set; }

        /// <summary>
        /// Gets or sets the vitals.
        /// </summary>
        /// <value>
        /// The vitals.
        /// </value>
        public IList<VitalMessage> Vitals { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public IList<NoteMessage> Notes { get; set; }

        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        /// <value>
        /// The device.
        /// </value>
        public DeviceMessage Device { get; set; }

        /// <summary>
        /// Gets or sets the type of the processing.
        /// </summary>
        /// <value>
        /// The type of the processing.
        /// </value>
        public string ProcessingType { get; set; }        
    }
}