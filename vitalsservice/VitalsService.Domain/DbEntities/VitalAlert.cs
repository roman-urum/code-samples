using System;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// VitalAlert.
    /// </summary>
    public class VitalAlert : Alert
    {
        /// <summary>
        /// Gets or sets the threshold identifier.
        /// </summary>
        /// <value>
        /// The threshold identifier.
        /// </value>
        public Guid ThresholdId { get; set; }

        /// <summary>
        /// Gets or sets the threshold.
        /// </summary>
        /// <value>
        /// The threshold.
        /// </value>
        public virtual Threshold Threshold { get; set; }

        /// <summary>
        /// Gets or sets the vital.
        /// </summary>
        /// <value>
        /// The vital.
        /// </value>
        public virtual Vital Vital { get; set; }
    }
}