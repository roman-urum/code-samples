using VitalsService.CustomAttributes;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Enums
{
    /// <summary>
    /// GetDeleteThresholdStatus.
    /// </summary>
    public enum GetDeleteThresholdStatus
    {
        [DescriptionLocalized("ThresholdResult_Success", typeof(GlobalStrings))]
        Success = 1,

        [DescriptionLocalized("ThresholdResult_ThresholdWithSuchIdDoesNotExist", typeof(GlobalStrings))]
        NotFound = 2
    }
}