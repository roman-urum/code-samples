using System;
using System.Collections.Generic;

namespace Maestro.Domain.Dtos.VitalsService.Alerts
{
    /// <summary>
    /// PatientAlertsDto.
    /// </summary>
    public class PatientAlertsDto
    {
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the alerts.
        /// </summary>
        /// <value>
        /// The alerts.
        /// </value>
        public IList<BaseAlertResponseDto> Alerts { get; set; }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public long Count
        {
            get
            {
                return Alerts != null ? Alerts.Count : 0;
            }
        }
    }
}