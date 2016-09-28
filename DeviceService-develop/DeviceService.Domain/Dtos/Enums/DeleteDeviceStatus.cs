using System;

using DeviceService.Common.CustomAttributes;
using DeviceService.Domain.Resources;

namespace DeviceService.Domain.Dtos.Enums
{
    [Flags]
    public enum DeleteDeviceStatus
    {
        Success = 1,

        [DescriptionLocalized("SetDecomissionStatus_DeviceNotFound", typeof(GlobalStrings))]
        DeviceNotFound = 2,

        [DescriptionLocalized("SetDecomissionStatus_InvalidDeviceStatus", typeof(GlobalStrings))]
        InvalidDeviceStatus = 4
    }
}