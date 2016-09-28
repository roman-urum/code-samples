using HealthLibrary.Common.CustomAttributes;
using HealthLibrary.Domain.Resources;

namespace HealthLibrary.Domain.Dtos.Enums
{
    public enum UpdateMediaStatus
    {
        Success = 1,

        [DescriptionLocalized("MediaResult_NotFound", typeof(GlobalStrings))]
        NotFound = 2,

        [DescriptionLocalized("Media_InvalidContentOrSourceContentUrlProvided", typeof(GlobalStrings))]
        InvalidContentOrSourceContentUrlProvided = 3
    }
}