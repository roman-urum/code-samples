using System;
using System.Collections.Generic;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// Threshold.
    /// </summary>
    public class Threshold : Entity
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public decimal MinValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public decimal MaxValue { get; set; }

        /// <summary>
        /// Gets or sets the alert severity identifier.
        /// </summary>
        /// <value>
        /// The alert severity identifier.
        /// </value>
        public Guid? AlertSeverityId { get; set; }

        /// <summary>
        /// Gets or sets the alert severity.
        /// </summary>
        /// <value>
        /// The alert severity.
        /// </value>
        public virtual AlertSeverity AlertSeverity { get; set; }

        /// <summary>
        /// Gets or sets the vital details.
        /// </summary>
        /// <value>
        /// The vital details.
        /// </value>
        public virtual ICollection<VitalAlert> VitalAlerts { get; set; }
    }
}