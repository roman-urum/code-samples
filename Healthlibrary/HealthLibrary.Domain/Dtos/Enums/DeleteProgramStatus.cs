using HealthLibrary.Common.CustomAttributes;
using HealthLibrary.Domain.Resources;

namespace HealthLibrary.Domain.Dtos.Enums
{
    public enum DeleteProgramStatus
    {
        Success = 1,

        [DescriptionLocalized("Program_NotFound", typeof(GlobalStrings))]
        NotFound = 2
    }
}