using System;
using VitalsService.CustomAttributes;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Enums
{
    /// <summary>
    /// CreateUpdateThresholdStatus.
    /// </summary>
    [Flags]
    public enum GetHealthSessionStatus
    {
        Success = 1,

        [DescriptionLocalized("HealthSession_NotFound", typeof(GlobalStrings))]
        HealthSessionNotFound = 2
    }
}