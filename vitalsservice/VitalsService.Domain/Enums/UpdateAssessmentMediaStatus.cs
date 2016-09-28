using VitalsService.CustomAttributes;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Enums
{
    public enum UpdateAssessmentMediaStatus
    {
        Success = 1,

        [DescriptionLocalized("AssessmentMedia_NotFoundError", typeof(GlobalStrings))]
        NotFound,

        [DescriptionLocalized("AssessmentMedia_FileAlreadyUploadedError", typeof(GlobalStrings))]
        FileAlreadyUploaded,

        [DescriptionLocalized("AssessmentMedia_InvalidContentProvidedError", typeof(GlobalStrings))]
        InvalidContentProvided
    }
}
