using System;
using HealthLibrary.Common.CustomAttributes;
using HealthLibrary.Domain.Resources;

namespace HealthLibrary.Domain.Dtos.Enums
{
    [Flags]
    public enum UpdateScaleAnswerSetLocalization
    {
        [DescriptionLocalized("ScaleAnswerSetResult_Success", typeof(GlobalStrings))]
        Success = 1 << 1,
        
        [DescriptionLocalized("ScaleAnsweSetResult_NotFound", typeof(GlobalStrings))]
        NotFound = 1 << 2
    }
}