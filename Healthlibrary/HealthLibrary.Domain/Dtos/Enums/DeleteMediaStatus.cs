using HealthLibrary.Common.CustomAttributes;
using HealthLibrary.Domain.Resources;

namespace HealthLibrary.Domain.Dtos.Enums
{
    public enum DeleteMediaStatus
    {
        [DescriptionLocalized("MediaResult_Success", typeof(GlobalStrings))]
        Success = 1,

        [DescriptionLocalized("MediaResult_NotFound", typeof(GlobalStrings))]
        NotFound = 2,

        [DescriptionLocalized("MediaResult_IsInUse", typeof(GlobalStrings))]
        InUse = 3,

        [DescriptionLocalized("MediaResult_ContentCannotBeRemovedFromStorage", typeof(GlobalStrings))]
        ContentCannotBeRemovedFromStorage = 4
    }
}