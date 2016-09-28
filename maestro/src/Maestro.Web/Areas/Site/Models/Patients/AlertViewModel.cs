using System;
using Maestro.Domain.Dtos.VitalsService.Alerts;
using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Web.Areas.Site.Models.Patients
{
    /// <summary>
    /// AlertViewModel.
    /// </summary>
    public class AlertViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public AlertType Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AlertViewModel"/> is acknowledged.
        /// </summary>
        /// <value>
        ///   <c>true</c> if acknowledged; otherwise, <c>false</c>.
        /// </value>
        public bool Acknowledged { get; set; }

        /// <summary>
        /// Gets or sets the alert severity.
        /// </summary>
        /// <value>
        /// The alert severity.
        /// </value>
        public AlertSeverityViewModel AlertSeverity { get; set; }

        /// <summary>
        /// Gets or sets the violated threshold.
        /// </summary>
        /// <value>
        /// The violated threshold.
        /// </value>
        public ViolatedThresholdDto ViolatedThreshold { get; set; }

        /// <summary>
        /// Gets or sets the occurred date.
        /// </summary>
        /// <value>
        /// The occurred date.
        /// </value>
        public DateTimeOffset Occurred { get; set; }
    }
}