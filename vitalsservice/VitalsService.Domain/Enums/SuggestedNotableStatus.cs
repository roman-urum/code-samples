using System;
using VitalsService.CustomAttributes;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Enums
{
    /// <summary>
    /// SuggestedNotableStatus.
    /// </summary>
    [Flags]
    public enum SuggestedNotableStatus
    {
        Success = 1 << 1,

        [DescriptionLocalized("SuggestedNotableStatus_NameConflict", typeof(GlobalStrings))]
        NameConflict = 1 << 2,

        [DescriptionLocalized("SuggestedNotableStatus_NotFound", typeof(GlobalStrings))]
        NotFound = 1 << 3
    }
}