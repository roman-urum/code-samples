using VitalsService.CustomAttributes;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Enums
{
    /// <summary>
    /// CreateMeasurementStatus.
    /// </summary>
    public enum CreateMeasurementStatus
    {
        Success = 1,

        [DescriptionLocalized("Measurement_MeasurementWithClientIdAlreadyExists", typeof(GlobalStrings))]
        MeasurementWithClientIdAlreadyExists = 2
    }
}