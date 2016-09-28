using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos.VitalsService.AlertSeverities;
using Maestro.Domain.Dtos.VitalsService.Conditions;
using Maestro.Domain.Dtos.VitalsService.Thresholds;

namespace Maestro.Web.Areas.Site.Models.Patients
{
    /// <summary>
    /// PatientThresholdsViewModel.
    /// </summary>
    public class PatientThresholdsViewModel
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the alert severities.
        /// </summary>
        /// <value>
        /// The alert severities.
        /// </value>
        public IList<AlertSeverityResponseDto> AlertSeverities { get; set; }

        /// <summary>
        /// Gets or sets the thresholds.
        /// </summary>
        /// <value>
        /// The thresholds.
        /// </value>
        public IList<BaseThresholdDto> Thresholds { get; set; }

        /// <summary>
        /// Gets or sets the list of conditions assigned to patient.
        /// </summary>
        /// <value>
        /// The list of conditions assigned to patient.
        /// </value>
        public IList<ConditionResponseDto> PatientConditions { get; set; }
    }
}