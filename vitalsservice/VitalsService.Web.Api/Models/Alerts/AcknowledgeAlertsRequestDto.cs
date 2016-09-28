using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VitalsService.Web.Api.DataAnnotations;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models.Alerts
{
    /// <summary>
    /// AcknowledgeAlertsRequestDto.
    /// </summary>
    public class AcknowledgeAlertsRequestDto
    {
        /// <summary>
        /// Gets or sets the acknowledged by.
        /// </summary>
        /// <value>
        /// The acknowledged by.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public Guid? AcknowledgedBy { get; set; }

        /// <summary>
        /// Gets or sets the alert ids.
        /// </summary>
        /// <value>
        /// The alert ids.
        /// </value>
        [ItemsRequired(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "ItemsRequired_ValidationError",
            ErrorMessage = null
        )]
        public IList<Guid> AlertIds { get; set; }
    }
}