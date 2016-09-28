using System;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// MeasurementNote.
    /// </summary>
    public class MeasurementNote : Entity
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the measurement identifier.
        /// </summary>
        /// <value>
        /// The measurement identifier.
        /// </value>
        public Guid MeasurementId { get; set; }

        /// <summary>
        /// Gets or sets the measurement.
        /// </summary>
        /// <value>
        /// The measurement.
        /// </value>
        public virtual Measurement Measurement { get; set; }
    }
}