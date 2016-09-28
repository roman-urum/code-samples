using System;
using VitalsService.CustomAttributes;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Enums
{
    /// <summary>
    /// CreateUpdateDeleteAlertSeverityStatus.
    /// </summary>
    [Flags]
    public enum CreateUpdateDeleteAlertSeverityStatus
    {
        [DescriptionLocalized("AlertSeverityResult_Success", typeof(GlobalStrings))]
        Success = 1,

        [DescriptionLocalized("AlertSeverityResult_NotFound", typeof(GlobalStrings))]
        NotFound = 2
    }
}