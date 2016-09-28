using HealthLibrary.Common.CustomAttributes;
using HealthLibrary.Domain.Resources;

namespace HealthLibrary.Domain.Dtos.Enums
{
    public enum CreateUpdateProgramStatus
    {
        Success = 1,

        [DescriptionLocalized("Program_NotFound", typeof(GlobalStrings))]
        NotFound,

        [DescriptionLocalized("Program_InvalidRecurrenceReferenceProvided", typeof(GlobalStrings))]
        InvalidRecurrenceReferenceProvided,

        [DescriptionLocalized("Program_InvalidProtolId", typeof(GlobalStrings))]
        InvalidProtocolId
    }
}