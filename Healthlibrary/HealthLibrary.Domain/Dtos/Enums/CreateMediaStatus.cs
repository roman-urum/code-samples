using HealthLibrary.Common.CustomAttributes;
using HealthLibrary.Domain.Resources;

namespace HealthLibrary.Domain.Dtos.Enums
{
    public enum CreateMediaStatus
    {
        Success = 1,

        [DescriptionLocalized("Media_InvalidContentOrSourceContentUrlProvided", typeof(GlobalStrings))]
        InvalidContentOrSourceContentUrlProvided = 2
    }
}