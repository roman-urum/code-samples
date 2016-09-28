using System.Collections.Generic;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// AlertSeverity.
    /// </summary>
    public class AlertSeverity : Entity
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the colour code.
        /// </summary>
        /// <value>
        /// The colour code.
        /// </value>
        public string ColorCode { get; set; }

        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        /// <value>
        /// The severity.
        /// </value>
        public int Severity { get; set; }

        /// <summary>
        /// Gets or sets the thresholds.
        /// </summary>
        /// <value>
        /// The thresholds.
        /// </value>
        public virtual ICollection<Threshold> Thresholds { get; set; }

        /// <summary>
        /// Gets or sets the alerts.
        /// </summary>
        /// <value>
        /// The alerts.
        /// </value>
        public virtual ICollection<Alert> Alerts { get; set; }
    }
}