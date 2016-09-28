using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.DataAnnotations;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models
{
    /// <summary>
    /// Model for data of reqest to create new measurement.
    /// </summary>
    public class MeasurementRequestDto
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MeasurementRequestDto"/> is automated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if automated; otherwise, <c>false</c>.
        /// </value>
        public bool IsAutomated { get; set; }

        /// <summary>
        /// Raw measurement from device.
        /// </summary> 
        public object RawJson { get; set; }

        /// <summary>
        /// Date & Time the vital was actually captured.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public DateTime? ObservedUtc { get; set; }

        /// <summary>
        /// Gets or sets the observed timezone.
        /// </summary>
        /// <value>
        /// The observed timezone.
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
        public string ObservedTz { get; set; }

        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        /// <value>
        /// The device.
        /// </value>
        public DeviceDto Device { get; set; }

        /// <summary>
        /// Gets or sets the type of the processing.
        /// </summary>
        /// <value>
        /// The type of the processing.
        /// </value>
        public ProcessingType? ProcessingType { get; set; }

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

        /// <summary>
        /// Actual measurement fields.
        /// </summary>
        [ItemsRequired]
        public IList<VitalDto> Vitals { get; set; }

        /// <summary>
        /// Measurement additional information.
        /// </summary>
        public IList<MeasurementNoteDto> MeasurementNotes { get; set; }
    }
}