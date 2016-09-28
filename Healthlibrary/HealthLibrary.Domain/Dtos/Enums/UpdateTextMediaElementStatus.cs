using HealthLibrary.Common.CustomAttributes;
using HealthLibrary.Domain.Resources;

namespace HealthLibrary.Domain.Dtos.Enums
{
    public enum UpdateTextMediaElementStatus
    {
        [DescriptionLocalized("TextMediaElement_Success", typeof(GlobalStrings))]
        Success = 1,

        [DescriptionLocalized("TextMediaElement_NotFound", typeof(GlobalStrings))]
        NotFound,

        [DescriptionLocalized("TextMediaElement_MediaFileMissed", typeof(GlobalStrings))]
        MediaFileMissed,

        [DescriptionLocalized("TextMediaElement_MediaTypeMissed", typeof(GlobalStrings))]
        MediaMediaType,

        [DescriptionLocalized("TextMediaElement_MediaFileNotFound", typeof(GlobalStrings))]
        MediaFileNotFound,

        [DescriptionLocalized("TextMediaElement_TextOrMediaMissed", typeof(GlobalStrings))]
        TextOrMediaMissed,

        [DescriptionLocalized("TextMediaElement_DupplicateTags", typeof(GlobalStrings))]
        DupplicateTags,

        [DescriptionLocalized("TextMediaElement_TagLengthMustBeLessThan", typeof(GlobalStrings))]
        TagLengthMustBeLessThan
    }
}