using Maestro.Domain.Dtos.VitalsService.Enums;
using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.VitalsService.Alerts
{
    [JsonObject]
    public class SearchAlertsDto : BaseSearchDto
    {
        /// <summary>
        /// Identifies if only base data of alerts should be loaded.
        /// </summary>
        public bool IsBrief { get; set; }

        /// <summary>
        /// Gets or sets the patient ids.
        /// </summary>
        /// <value>
        /// The patient ids.
        /// </value>
        public IList<Guid> PatientIds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SearchAlertsDto"/> is acknowledged.
        /// </summary>
        /// <value>
        ///   <c>true</c> if acknowledged; otherwise, <c>false</c>.
        /// </value>
        public bool Acknowledged { get; set; }

        /// <summary>
        /// Gets or sets the acknowledged from.
        /// </summary>
        /// <value>
        /// The acknowledged from.
        /// </value>
        public DateTime? AcknowledgedFrom { get; set; }

        /// <summary>
        /// Gets or sets the acknowledged to.
        /// </summary>
        /// <value>
        /// The acknowledged to.
        /// </value>
        public DateTime? AcknowledgedTo { get; set; }

        /// <summary>
        /// Gets or sets the list of types.
        /// </summary>
        /// <value>
        /// The list of types.
        /// </value>
        public IList<AlertType> Types { get; set; }

        /// <summary>
        /// Gets or sets the severity dentifiers.
        /// </summary>
        /// <value>
        /// The severity dentifiers.
        /// </value>
        public IList<Guid> SeverityIds { get; set; }
    }
}
