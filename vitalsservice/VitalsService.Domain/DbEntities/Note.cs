using System;
using System.Collections.Generic;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// Note.
    /// </summary>
    public class Note : Entity, IAuditable
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

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
        /// Gets or sets the vital identifier.
        /// </summary>
        /// <value>
        /// The vital identifier.
        /// </value>
        public Guid? MeasurementId { get; set; }

        /// <summary>
        /// Gets or sets the vital.
        /// </summary>
        /// <value>
        /// The vital.
        /// </value>
        public virtual Measurement Measurement { get; set; }

        /// <summary>
        /// Gets or sets the health session element identifier.
        /// </summary>
        /// <value>
        /// The health session element identifier.
        /// </value>
        public Guid? HealthSessionElementId { get; set; }

        /// <summary>
        /// Gets or sets the health session element.
        /// </summary>
        /// <value>
        /// The health session element.
        /// </value>
        public virtual HealthSessionElement HealthSessionElement { get; set; }

        /// <summary>
        /// Gets or sets the notables.
        /// </summary>
        /// <value>
        /// The notables.
        /// </value>
        public virtual ICollection<NoteNotable> Notables { get; set; }
    }
}