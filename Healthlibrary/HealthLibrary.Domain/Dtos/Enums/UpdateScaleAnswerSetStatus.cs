using System;
using HealthLibrary.Common.CustomAttributes;
using HealthLibrary.Domain.Resources;

namespace HealthLibrary.Domain.Dtos.Enums
{
    [Flags]
    public enum UpdateScaleAnswerSetStatus
    {
        [DescriptionLocalized("ScaleAnswerSetResult_Success", typeof(GlobalStrings))]
        Success = 1 << 1,

        [DescriptionLocalized("ScaleAnsweSetResult_NotFound", typeof(GlobalStrings))]
        NotFound = 1 << 2,

        [DescriptionLocalized("ScaleAnsweSetResult_LowLabelLanguageNotMatch", typeof(GlobalStrings))]
        LowLabelLanguageNotMatch = 1 << 3,

        [DescriptionLocalized("ScaleAnsweSetResult_MidLabelLanguageNotMatch", typeof(GlobalStrings))]
        MidLabelLanguageNotMatch = 1 << 4,

        [DescriptionLocalized("ScaleAnsweSetResult_HighLabelLanguageNotMatch", typeof(GlobalStrings))]
        HighLabelLanguageNotMatch = 1 << 5,

        [DescriptionLocalized("ScaleAnswerSetResult_HighLabelIsRequired", typeof(GlobalStrings))]
        HighLabelIsRequired = 1 << 6,
     
        [DescriptionLocalized("ScaleAnswerSetResult_LowLabelIsRequired", typeof(GlobalStrings))]
        LowLabelIsRequired = 1 << 7,
    }
}
