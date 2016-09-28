using DeviceService.Common.CustomAttributes;
using DeviceService.Domain.Resources;

namespace DeviceService.Domain.Dtos.Enums
{
    /// <summary>
    /// CreateDeviceStatus.
    /// </summary>
    public enum CreateDeviceStatus
    {
        Success = 1,

        [DescriptionLocalized("CreteDeviceStatus_IVRAlreadyExists", typeof(GlobalStrings))]
        IVRAlreadyExists = 2
    }
}