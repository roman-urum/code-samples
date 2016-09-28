using System;

using DeviceService.Common.CustomAttributes;
using DeviceService.Domain.Resources;

namespace DeviceService.Domain.Dtos.Enums
{
    [Flags]
    public enum SetDecomissionStatusOperationStatus
    {
        [DescriptionLocalized("SetDecomissionStatus_Success", typeof(GlobalStrings))]
        Success = 1,

        [DescriptionLocalized("SetDecomissionStatus_InvalidDecomissionStatus", typeof(GlobalStrings))]
        InvalidDecomissionStatus = 2,

        [DescriptionLocalized("SetDecomissionStatus_DeviceNotFound", typeof(GlobalStrings))]
        DeviceNotFound = 4,

        [DescriptionLocalized("SetDecomissionStatus_InvalidDeviceStatus", typeof(GlobalStrings))]
        InvalidDeviceStatus = 8
    }
}