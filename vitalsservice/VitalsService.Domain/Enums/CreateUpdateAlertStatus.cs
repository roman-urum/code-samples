using System;
using VitalsService.CustomAttributes;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Enums
{
    /// <summary>
    /// CreateUpdateAlertStatus.
    /// </summary>
    [Flags]
    public enum CreateUpdateAlertStatus
    {
        [DescriptionLocalized("AlertResult_Success", typeof(GlobalStrings))]
        Success = 1 << 1,

        [DescriptionLocalized("AlertResult_OneOfProvidedAlertsDoesNotExistOrAlreadyAcknowledged", typeof(GlobalStrings))]
        OneOfProvidedAlertsDoesNotExistOrAlreadyAcknowledged = 1 << 2,

        [DescriptionLocalized("AlertResult_AlertSeverityWithSuchIdDoesNotExist", typeof(GlobalStrings))]
        AlertSeverityWithSuchIdDoesNotExist = 1 << 3
    }
}