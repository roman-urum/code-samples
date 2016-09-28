using System;
using VitalsService.CustomAttributes;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Enums
{
    /// <summary>
    /// CreateUpdateThresholdStatus.
    /// </summary>
    [Flags]
    public enum CreateHealthSessionStatus
    {
        [DescriptionLocalized("HealthSession_Success", typeof(GlobalStrings))]
        Success = 1 << 1,

        [DescriptionLocalized("HealthSession_AssessmentMediaIsNotValid", typeof(GlobalStrings))]
        AssessmentMediaIsNotValid = 1 << 2,

        [DescriptionLocalized("HealthSession_HealthSessionWithClientIdAlreadyExists", typeof(GlobalStrings))]
        HealthSessionWithClientIdAlreadyExists = 1 << 3
    }
}