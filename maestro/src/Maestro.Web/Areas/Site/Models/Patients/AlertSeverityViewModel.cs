using System;

namespace Maestro.Web.Areas.Site.Models.Patients
{
    /// <summary>
    /// AlertSeverityViewModel.
    /// </summary>
    public class AlertSeverityViewModel
    {
        /// <summary>
        /// Gets or sets the ientifier.
        /// </summary>
        /// <value>
        /// The ientifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the color code.
        /// </summary>
        /// <value>
        /// The color code.
        /// </value>
        public string ColorCode { get; set; }

        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        /// <value>
        /// The severity.
        /// </value>
        public int Severity { get; set; }
    }
}