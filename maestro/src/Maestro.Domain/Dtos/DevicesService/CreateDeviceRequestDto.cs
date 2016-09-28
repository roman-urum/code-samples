using Maestro.Domain.Dtos.DevicesService.Enums;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.DevicesService
{
    /// <summary>
    /// CreateDeviceRequestDtoю
    /// </summary>
    [JsonObject]
    public class CreateDeviceRequestDto : BaseDeviceRequestDto
    {
        /// <summary>
        /// Type of patient device.
        /// </summary>
        public DeviceType DeviceType { get; set; }
    }
}