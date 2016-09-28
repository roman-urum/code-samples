using System;
using System.ComponentModel.DataAnnotations;
using FoolproofWebApi;
using VitalsService.Domain.Constants;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Resources;
using VitalsService.Web.Api.DataAnnotations;

namespace VitalsService.Web.Api.Models.HealthSessions
{
    /// <summary>
    /// Base model for health session 
    /// </summary>
    public class BaseHealthSessionDto
    {
        /// <summary>
        /// GUID of the protocol being taken.
        /// </summary>
        [RequiredIfNotEmpty(
            "ProtocolName",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredIfNotEmptyAttribute_ValidationError",
            ErrorMessage = null
        )]
        [ForbidDefaultNotNullableValue(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "ForbidDefaultNotNullableValueAttribute_ValidationError",
            ErrorMessage = null
        )]
        public Guid? ProtocolId { get; set; }

        /// <summary>
        /// Name of the protocol being taken
        /// </summary>
        [StringLength(
            DbConstraints.MaxLength.ProtocolName,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        [RequiredIfNotEmpty(
            "ProtocolId",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredIfNotEmptyAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string ProtocolName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is private.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is private; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is incomplete.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is incomplete; otherwise, <c>false</c>.
        /// </value>
        public bool IsIncomplete { get; set; }

        /// <summary>
        /// Debugging, Testing or Production
        /// </summary>
        public ProcessingType ProcessingType { get; set; }

        /// <summary>
        /// The UTC date/time this health session was scheduled for.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public DateTime? ScheduledUtc { get; set; }

        /// <summary>
        /// Gets or sets the scheduled timezone.
        /// </summary>
        /// <value>
        /// The scheduled timezone.
        /// </value>
        [IANATimeZone(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "IANATimeZoneAttribute_ValidationError",
            ErrorMessage = null
        )]
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [StringLength(
            44,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError"
        )]
        public string ScheduledTz { get; set; }

        /// <summary>
        /// The calendar item ID that triggered this health session
        /// </summary>
        public Guid? CalendarItemId { get; set; }

        /// <summary>
        /// The UTC date/time this health session was started by patient
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public DateTime? StartedUtc { get; set; }

        /// <summary>
        /// Gets or sets the started timezone.
        /// </summary>
        /// <value>
        /// The started timezone.
        /// </value>
        [IANATimeZone(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "IANATimeZoneAttribute_ValidationError",
            ErrorMessage = null
        )]
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [StringLength(
            44,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError"
        )]
        public string StartedTz { get; set; }

        /// <summary>
        /// The UTC date/time this health session was completed by patient
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [CustomAttributes.GreaterThan(
            "StartedUtc",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "GreaterThanAttribute_ValidationError",
            ErrorMessage = null)]
        public DateTime? CompletedUtc { get; set; }

        /// <summary>
        /// Gets or sets the completed timezone.
        /// </summary>
        /// <value>
        /// The completed timezone.
        /// </value>
        [IANATimeZone(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "IANATimeZoneAttribute_ValidationError",
            ErrorMessage = null
        )]
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [StringLength(
            44,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError"
        )]
        public string CompletedTz { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        [StringLength(
            50,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError"
        )]
        public string ClientId { get; set; }
    }
}