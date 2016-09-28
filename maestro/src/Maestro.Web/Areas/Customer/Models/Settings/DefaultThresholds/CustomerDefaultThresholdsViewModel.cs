using System.Collections.Generic;
using Maestro.Domain.Dtos.VitalsService.AlertSeverities;
using Maestro.Domain.Dtos.VitalsService.Thresholds;

namespace Maestro.Web.Areas.Customer.Models.Settings.DefaultThresholds
{
    /// <summary>
    /// CustomerDefaultThresholdsViewModel.
    /// </summary>
    public class CustomerDefaultThresholdsViewModel
    {
        /// <summary>
        /// Gets or sets the alert severities.
        /// </summary>
        /// <value>
        /// The alert severities.
        /// </value>
        public IList<AlertSeverityResponseDto> AlertSeverities { get; set; }

        /// <summary>
        /// Gets or sets the default thresholds.
        /// </summary>
        /// <value>
        /// The default thresholds.
        /// </value>
        public IList<DefaultThresholdDto> DefaultThresholds { get; set; }
    }
}