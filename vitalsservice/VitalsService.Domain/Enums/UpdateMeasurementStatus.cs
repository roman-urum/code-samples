using System;
using VitalsService.CustomAttributes;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Enums
{
    /// <summary>
    /// CreateUpdateThresholdStatus.
    /// </summary>
    [Flags]
    public enum UpdateMeasurementStatus
    {
        Success = 1,

        [DescriptionLocalized("Measuerment_NotFound", typeof(GlobalStrings))]
        MeasurementNotFound = 2
    }
}