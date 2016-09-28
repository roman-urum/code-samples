using System;
using VitalsService.CustomAttributes;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Enums
{
    /// <summary>
    /// CreateUpdateThresholdStatus.
    /// </summary>
    [Flags]
    public enum CreateUpdateThresholdStatus
    {
        [DescriptionLocalized("ThresholdResult_Success", typeof(GlobalStrings))]
        Success = 1 << 1,

        [DescriptionLocalized("ThresholdResult_VitalThresholdAlreadyExists", typeof(GlobalStrings))]
        VitalThresholdAlreadyExists = 1 << 2,

        [DescriptionLocalized("ThresholdResult_VitalThresholdDoesNotExist", typeof(GlobalStrings))]
        VitalThresholdDoesNotExist = 1 << 3,

        [DescriptionLocalized("ThresholdResult_AlertSeverityDoesNotExist", typeof(GlobalStrings))]
        AlertSeverityDoesNotExist = 1 << 4,

        [DescriptionLocalized("ThresholdResult_ExistingAlertSeverityShouldBeUsed", typeof(GlobalStrings))]
        ExistingAlertSeverityShouldBeUsed = 1 << 5
    }
}