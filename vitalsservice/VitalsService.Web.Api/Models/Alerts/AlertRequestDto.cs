using System;
using System.ComponentModel.DataAnnotations;
using VitalsService.CustomAttributes;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models.Alerts
{
    /// <summary>
    /// AlertRequestDto.
    /// </summary>
    public class AlertRequestDto
    {
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public Guid? PatientId { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public AlertType? Type { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [MaxLength(
            250,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        [MaxLength(
            5000,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Body { get; set; }        

        /// <summary>
        /// Gets or sets the occurred UTC.
        /// </summary>
        /// <value>
        /// The occurred UTC.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public DateTime OccurredUtc { get; set; }

        /// <summary>
        /// Gets or sets the expires UTC.
        /// </summary>
        /// <value>
        /// The expires UTC.
        /// </value>
        [GreaterThan(
            "OccurredUtc",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "GreaterThanAttribute_ValidationError",
            ErrorMessage = null,
            PassOnNull = true
        )]
        public DateTime? ExpiresUtc { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        [Range(
            0,
            int.MaxValue,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "GreaterThanZero_ValidationError",
            ErrorMessage = null
        )]
        public int Weight { get; set; }

        /// <summary>
        /// Gets or sets the alert severity identifier.
        /// </summary>
        /// <value>
        /// The alert severity identifier
        /// </value>
        public Guid? AlertSeverityId { get; set; }
    }
}