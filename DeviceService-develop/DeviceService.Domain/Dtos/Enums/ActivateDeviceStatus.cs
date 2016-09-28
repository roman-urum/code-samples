using System;
using DeviceService.Common.CustomAttributes;
using DeviceService.Domain.Resources;

namespace DeviceService.Domain.Dtos.Enums
{
    [Flags]
    public enum ActivateDeviceStatus
    {
        Success = 1,

        [DescriptionLocalized("SetDecomissionStatus_DeviceNotFound", typeof(GlobalStrings))]
        DeviceNotFound = 2,

        [DescriptionLocalized("SetDecomissionStatus_InvalidDeviceStatus", typeof(GlobalStrings))]
        InvalidaDeviceStatus = 4,

        [DescriptionLocalized("SetDecomissionStatus_CertificateSigningRequestInvalid", typeof(GlobalStrings))]
        CertificateSigningRequestInvalid = 8
    }
}