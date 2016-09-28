using System;
using Maestro.Web.Areas.Site.Models.Patients;

namespace Maestro.Web.Areas.Site.Models
{
    /// <summary>
    /// ThresholdViewModel.
    /// </summary>
    public class ThresholdViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public decimal MaxValue { get; set; }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public decimal MinValue { get; set; }

        /// <summary>
        /// Gets or sets the alert severity.
        /// </summary>
        /// <value>
        /// The alert severity.
        /// </value>
        public AlertSeverityViewModel AlertSeverity { get; set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets the falg which defines is threshold default or not
        /// </summary>
        /// <value>
        ///  The falg which defines is threshold default or not
        /// </value>
        public bool IsDefault { get; set; }
    }
}