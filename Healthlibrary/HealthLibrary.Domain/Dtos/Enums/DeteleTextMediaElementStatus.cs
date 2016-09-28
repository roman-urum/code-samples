using HealthLibrary.Common.CustomAttributes;
using HealthLibrary.Domain.Resources;

namespace HealthLibrary.Domain.Dtos.Enums
{
    public enum DeteleTextMediaElementStatus
    {
        [DescriptionLocalized("TextMediaElement_DeleteSuccess", typeof(GlobalStrings))]
        Success = 1,

        [DescriptionLocalized("TextMediaElement_NotFound", typeof(GlobalStrings))]
        NotFound,

        [DescriptionLocalized("TextMediaElement_IsInUse", typeof(GlobalStrings))]
        ElementIsInUse,

        [DescriptionLocalized("Common_DeleteLocaleForbidden", typeof(GlobalStrings))]
        DeleteLocaleForbidden
    }
}