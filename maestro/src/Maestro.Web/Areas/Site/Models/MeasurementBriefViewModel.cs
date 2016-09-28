using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos.VitalsService;
using Maestro.Web.Areas.Site.Models.Dashboard;

namespace Maestro.Web.Areas.Site.Models
{
    /// <summary>
    /// MeasurementBriefViewModel.
    /// </summary>
    public class MeasurementBriefViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        /// <value>
        /// The updated.
        /// </value>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Gets or sets the observed.
        /// </summary>
        /// <value>
        /// The observed.
        /// </value>
        public DateTimeOffset Observed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MeasurementBriefViewModel"/> is invalidated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if invalidated; otherwise, <c>false</c>.
        /// </value>
        public bool IsInvalidated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MeasurementBriefViewModel"/> is automated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if automated; otherwise, <c>false</c>.
        /// </value>
        public bool IsAutomated { get; set; }

        /// <summary>
        /// List of all vitals in measurement.
        /// </summary>
        public IList<VitalViewModel> Vitals { get; set; }
    }
}