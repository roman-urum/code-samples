using System;
using VitalsService.CustomAttributes;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Enums
{
    /// <summary>
    /// CreateUpdatePatientConditionsStatus.
    /// </summary>
    [Flags]
    public enum CreateUpdatePatientConditionsStatus
    {
        Success = 1 << 1,

        [DescriptionLocalized("CreateUpdatePatientConditionsStatus_OneOfProvidedConditionsInvalid", typeof(GlobalStrings))]
        OneOfProvidedConditionsInvalid = 1 << 2
    }
}