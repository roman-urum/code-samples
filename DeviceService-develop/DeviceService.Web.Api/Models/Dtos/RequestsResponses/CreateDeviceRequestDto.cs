using System.ComponentModel.DataAnnotations;
using DeviceService.Domain.Entities.Enums;
using DeviceService.Web.Api.Resources;

namespace DeviceService.Web.Api.Models.Dtos.RequestsResponses
{
    /// <summary>
    /// CreateDeviceRequestDtoю
    /// </summary>
    public class CreateDeviceRequestDto : BaseDeviceRequestDto
    {
        /// <summary>
        /// Type of patient device.
        /// Use 'Other' if type is unknown.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public DeviceType DeviceType { get; set; }
    }
}