using System;
using System.ComponentModel.DataAnnotations;
using DeviceService.Web.Api.DataAnnotations;
using DeviceService.Web.Api.Models.Dtos.Entities;
using DeviceService.Web.Api.Resources;

namespace DeviceService.Web.Api.Models.Dtos.RequestsResponses
{
    /// <summary>
    /// Common fields for request objects with device info.
    /// </summary>
    public class BaseDeviceRequestDto
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
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError", 
            ErrorMessage = null
        )]
        [DateString(
            Format = "yyyy-MM-dd",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "BirthDate_RecognizeDate"
        )]
        public string BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the device model.
        /// </summary>
        /// <value>
        /// The device model.
        /// </value>
        public string DeviceModel { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public DeviceSettingsDto Settings { get; set; }
    }
}