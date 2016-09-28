using System;
using System.Collections.Generic;
using VitalsService.Domain.Enums;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// Measurement.
    /// </summary>
    public class Measurement : Entity, IAuditable
    {
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
        /// Gets or sets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTime CreatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        /// <value>
        /// The updated.
        /// </value>
        public DateTime UpdatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the observed.
        /// </summary>
        /// <value>
        /// The observed.
        /// </value>
        public DateTime ObservedUtc { get; set; }

        /// <summary>
        /// Gets or sets the observed timezone.
        /// </summary>
        /// <value>
        /// The observed timezone.
        /// </value>
        public string ObservedTz { get; set; }

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
        /// Gets or sets the type of the processing.
        /// </summary>
        /// <value>
        /// The type of the processing.
        /// </value>
        public ProcessingType ProcessingType { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the vitals.
        /// </summary>
        /// <value>
        /// The vitals.
        /// </value>
        public virtual ICollection<Vital> Vitals { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public virtual ICollection<MeasurementNote> MeasurementNotes { get; set; }

        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        /// <value>
        /// The device.
        /// </value>
        public virtual Device Device { get; set; }

        /// <summary>
        /// Gets or sets the measurement element value.
        /// </summary>
        public virtual ICollection<MeasurementValue> MeasurementValues { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public virtual ICollection<Note> Notes { get; set; }
    }
}