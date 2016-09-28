using System;
using System.Collections.Generic;
using VitalsService.Domain.DbEntities;

namespace VitalsService.Domain.DomainObjects
{
    /// <summary>
    /// PatientAlerts.
    /// </summary>
    public class PatientAlerts
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
        public IList<Alert> Alerts { get; set; }
    }
}