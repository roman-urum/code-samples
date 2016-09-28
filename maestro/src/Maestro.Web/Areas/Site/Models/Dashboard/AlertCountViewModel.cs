using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Web.Areas.Site.Models.Dashboard
{
    /// <summary>
    /// CardsCountViewModel class.
    /// </summary>
    public class AlertCountViewModel
    {
        /// <summary>
        /// Gets or sets the alert type
        /// </summary>
        public AlertType AlertType { get; set; }

        /// <summary>
        /// Gets or sets the list of alert severity counts
        /// </summary>
        public IList<AlertSeverityCountViewModel> AlertSeverityCounts { get; set; }

        /// <summary>
        /// Gets or sets the amout of alerts of given type
        /// </summary>
        public int AlertTypeCount { get; set; }

        /// <summary>
        /// Gets or sets the most recent date of highest severity.
        /// </summary>
        /// Note: This property is being used only for sorting on backend,
        /// so it isn't necessary to convert it to local timezone
        public DateTime MostRecentDateOfHighestSeverityUtc { get; set; }
    }
}