using VitalsService.CustomAttributes;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Enums
{
    public enum ConditionStatus
    {
        Success = 1 << 1,

        [DescriptionLocalized("ConditionsStatus_ConditionNotFound", typeof(GlobalStrings))]
        NotFound = 1 << 2,

        [DescriptionLocalized("ConditionsStatus_ConditionAlreadyExistsForThatCustomer", typeof(GlobalStrings))]
        ConditionAlreadyExists = 1 << 4
    }
}