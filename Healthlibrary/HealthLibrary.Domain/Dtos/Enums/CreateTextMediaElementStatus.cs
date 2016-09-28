using System;

using HealthLibrary.Common.CustomAttributes;
using HealthLibrary.Domain.Resources;

namespace HealthLibrary.Domain.Dtos.Enums
{
    [Flags]
    public enum CreateTextMediaElementStatus
    {
        [DescriptionLocalized("TextMediaElement_Success", typeof(GlobalStrings))]
        Success = 1 << 1,

        [DescriptionLocalized("TextMediaElement_MediaFileMissed", typeof(GlobalStrings))]
        MediaFileMissed = 1 << 2,

        [DescriptionLocalized("TextMediaElement_MediaTypeMissed", typeof(GlobalStrings))]
        MediaMediaType = 1 << 3,

        [DescriptionLocalized("TextMediaElement_MediaFileNotFound", typeof(GlobalStrings))]
        MediaFileNotFound = 1 << 4,

        [DescriptionLocalized("TextMediaElement_TextOrMediaMissed", typeof(GlobalStrings))]
        TextOrMediaMissed = 1 << 5,

        [DescriptionLocalized("TextMediaElement_DupplicateTags", typeof(GlobalStrings))]
        DupplicateTags = 1 << 6,

        [DescriptionLocalized("TextMediaElement_TagLengthMustBeLessThan", typeof(GlobalStrings))]
        TagLengthMustBeLessThan = 1 << 7
    }
}