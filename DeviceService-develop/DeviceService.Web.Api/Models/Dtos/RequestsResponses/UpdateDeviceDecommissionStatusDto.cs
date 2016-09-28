using System.ComponentModel.DataAnnotations;
using DeviceService.Web.Api.Models.Dtos.Enums;
using DeviceService.Web.Api.Resources;
using Newtonsoft.Json;

namespace DeviceService.Web.Api.Models.Dtos.RequestsResponses
{
    /// <summary>
    /// UpdateDeviceDecommissionStatusDtoю
    /// </summary>
    public class UpdateDeviceDecommissionStatusDto
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings), 
            ErrorMessageResourceName = "RequiredAttribute_ValidationError", 
            ErrorMessage = null
        )]
        [JsonProperty(PropertyName = "status")]
        public DecommissionStatusDto Status { get; set; }
    }
}