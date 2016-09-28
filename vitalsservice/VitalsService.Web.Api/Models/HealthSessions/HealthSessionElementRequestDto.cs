using System;
using System.ComponentModel.DataAnnotations;
using FoolproofWebApi;
using VitalsService.Web.Api.DataAnnotations;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models.HealthSessions
{
    /// <summary>
    /// HealthSessionElementRequestDto.
    /// </summary>
    public class HealthSessionElementRequestDto : BaseHealthSessionElementDto
    {
        /// <summary>
        /// The UTC date/time this element was answered.
        /// </summary>
        [RequiredIfTrue(
            "Alert",
            ErrorMessage = null,
            ErrorMessageResourceName = "NotNullAnsweredUtc_ValidationError",
            ErrorMessageResourceType = typeof(GlobalStrings)
        )]
        public DateTime? AnsweredUtc { get; set; }

        /// <summary>
        /// Gets or sets the answered timezone.
        /// </summary>
        /// <value>
        /// The answered timezone.
        /// </value>
        [IANATimeZone(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "IANATimeZoneAttribute_ValidationError",
            ErrorMessage = null
        )]
        [RequiredIfTrue(
            "Alert",
            ErrorMessage = null,
            ErrorMessageResourceName = "NotNullAnsweredUtc_ValidationError",
            ErrorMessageResourceType = typeof(GlobalStrings)
        )]
        [StringLength(
            44,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError"
        )]
        public string AnsweredTz { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HealthSessionElementRequestDto"/> is alert.
        /// </summary>
        /// <value>
        ///   <c>true</c> if alert; otherwise, <c>false</c>.
        /// </value>
        public bool Alert { get; set; }

        /// <summary>
        /// Gets or sets the alert severity identifier.
        /// </summary>
        /// <value>
        /// The alert severity identifier.
        /// </value>
        public Guid? AlertSeverityId { get; set; }
    }
}