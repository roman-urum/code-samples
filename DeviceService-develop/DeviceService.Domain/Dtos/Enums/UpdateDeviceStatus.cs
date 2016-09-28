using DeviceService.Common.CustomAttributes;
using DeviceService.Domain.Resources;

namespace DeviceService.Domain.Dtos.Enums
{
    /// <summary>
    /// UpdateDeviceStatus.
    /// </summary>
    public enum UpdateDeviceStatus
    {
        Success = 1,

        [DescriptionLocalized("SetDecomissionStatus_DeviceNotFound", typeof(GlobalStrings))]
        DeviceNotFound = 2,
        
        [DescriptionLocalized("UpdateDeviceStatus_PinCodeRequired", typeof(GlobalStrings))]
        PinCodeRequired = 3
    }
}