using System;
using VitalsService.CustomAttributes;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Enums
{
    /// <summary>
    /// CreateUpdateDefaultThresholdStatus.
    /// </summary>
    [Flags]
    public enum CreateUpdateDefaultThresholdStatus
    {
        [DescriptionLocalized("ThresholdResult_Success", typeof(GlobalStrings))]
        Success = 1 << 1,

        [DescriptionLocalized("ThresholdResult_VitalThresholdAlreadyExists", typeof(GlobalStrings))]
        VitalDefaultThresholdAlreadyExists = 1 << 2,

        [DescriptionLocalized("ThresholdResult_VitalThresholdDoesNotExist", typeof(GlobalStrings))]
        VitalDefaultThresholdDoesNotExist = 1 << 3,

        [DescriptionLocalized("ThresholdResult_AlertSeverityDoesNotExist", typeof(GlobalStrings))]
        AlertSeverityDoesNotExist = 1 << 4,

        [DescriptionLocalized("ThresholdResult_ExistingAlertSeverityShouldBeUsed", typeof(GlobalStrings))]
        ExistingAlertSeverityShouldBeUsed = 1 << 5,

        [DescriptionLocalized("ThresholdResult_VitalConditionThresholdAlreadyExists", typeof(GlobalStrings))]
        VitalConditionThresholdAlreadyExists = 1 << 6,

        [DescriptionLocalized("ThresholdResult_ConditionDoesNotExist", typeof(GlobalStrings))]
        ConditionDoesNotExist = 1 << 7,

        [DescriptionLocalized("ThresholdResult_ConditionShouldBeSpecified", typeof(GlobalStrings))]
        ConditionShouldBeSpecified = 1 << 8

    }
}