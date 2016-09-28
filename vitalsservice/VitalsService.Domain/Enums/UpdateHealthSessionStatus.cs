using System;
using VitalsService.CustomAttributes;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Enums
{
    /// <summary>
    /// CreateUpdateThresholdStatus.
    /// </summary>
    [Flags]
    public enum UpdateHealthSessionStatus
    {
        [DescriptionLocalized("HealthSession_Success", typeof(GlobalStrings))]
        Success = 1 << 1,

        [DescriptionLocalized("HealthSession_AlertCreateError", typeof(GlobalStrings))]
        AlertCreateError = 1 << 2,

        [DescriptionLocalized("HealthSession_NotFound", typeof(GlobalStrings))]
        HealthSessionNotFound = 1 << 3
    }
}