using System;
using System.Collections.Generic;
using VitalsService.CustomAttributes;
using VitalsService.Domain.Enums;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Dtos
{
    /// <summary>
    /// AlertsSearchDto.
    /// </summary>
    public class AlertsSearchDto : BaseSearchDto
    {
        /// <summary>
        /// Determines if only base data of alerts is needed in response. True by default.
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
        /// Gets or sets a value indicating whether this <see cref="AlertsSearchDto"/> is acknowledged.
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
        [GreaterThan(
            "AcknowledgedFrom",
            PassOnNull = true,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "GreaterThanAttribute_ValidationError",
            ErrorMessage = null
        )]
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

        public AlertsSearchDto()
        {
            IsBrief = true;
        }
    }
}