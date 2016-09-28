using System.ComponentModel;
using DeviceService.Web.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using DeviceService.Domain.Entities.Enums;
using DeviceService.Web.Api.DataAnnotations;

namespace DeviceService.Web.Api.Models.Dtos.Entities
{
    /// <summary>
    /// ActivationDto.
    /// </summary>
    public class ActivationDto
    {
        /// <summary>
        /// Patient's activation code.
        /// </summary>
        /// <value>
        /// The activation code.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(GlobalStrings), 
            ErrorMessageResourceName = "RequiredAttribute_ValidationError", 
            ErrorMessage = null)]
        [JsonProperty(PropertyName = "code")]
        public string ActivationCode { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(GlobalStrings), 
            ErrorMessageResourceName = "RequiredAttribute_ValidationError", 
            ErrorMessage = null)]
        [JsonProperty(PropertyName = "dob")]
        [DateString(Format = "yyyy-MM-dd", ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "BirthDate_RecognizeDate")]
        public string BirthDate { get; set; }

        /// <summary>
        /// Base64-encoded certificate signing request.
        /// </summary>
        /// <value>
        /// The certificate.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(GlobalStrings), 
            ErrorMessageResourceName = "RequiredAttribute_ValidationError", 
            ErrorMessage = null)]
        [JsonProperty(PropertyName = "csr")]
        public string Certificate { get; set; }

        /// <summary>
        /// Device's unique identifier string.
        /// </summary>
        /// <value>
        /// The device identifier.
        /// </value>
        [JsonProperty(PropertyName = "deviceId")]
        public string DeviceId { get; set; }

        /// <summary>
        /// IMEI for cellular, MAC for non-cellular
        /// </summary>
        /// <value>
        /// The type of the device identifier.
        /// </value>
        [JsonProperty(PropertyName = "deviceIdType")]
        [Description("IMEI | MAC")]
        //[ApiAllowableValues2("deviceIdType", typeof(DeviceIdType))]
        [DeviceIdTypeValidation]
        public DeviceIdType? DeviceIdType { get; set; }

        /// <summary>
        /// Type of patient device.
        /// </summary>
        [JsonProperty(PropertyName = "deviceType")]
        public DeviceType? DeviceType { get; set; }

        /// <summary>
        /// Gets or sets the device model.
        /// </summary>
        /// <value>
        /// The device model.
        /// </value>
        [JsonProperty(PropertyName = "deviceModel")]
        public string DeviceModel { get; set; }
    }
}