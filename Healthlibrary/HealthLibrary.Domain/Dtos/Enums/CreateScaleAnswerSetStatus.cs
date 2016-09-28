using System;
using HealthLibrary.Common.CustomAttributes;
using HealthLibrary.Domain.Resources;

namespace HealthLibrary.Domain.Dtos.Enums
{
    [Flags]
    public enum CreateScaleAnswerSetStatus
    {
        [DescriptionLocalized("ScaleAnswerSetResult_Success", typeof(GlobalStrings))]
        Success = 1 << 1,
        [DescriptionLocalized("ScaleAnswerSetResult_HighLabelIsRequired", typeof(GlobalStrings))]
        HighLabelIsRequired = 1 << 2,
        [DescriptionLocalized("ScaleAnswerSetResult_MidLabelIsRequired", typeof(GlobalStrings))]
        MidLabelIsRequired = 1 << 3,
        [DescriptionLocalized("ScaleAnswerSetResult_LowLabelIsRequired", typeof(GlobalStrings))]
        LowLabelIsRequired = 1 << 4,
    }
}
