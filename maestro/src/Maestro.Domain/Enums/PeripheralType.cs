using Maestro.Common.CustomAttributes;
using Maestro.Domain.Resources;

namespace Maestro.Domain.Enums
{
    /// <summary>
    /// PeripheralType.
    /// </summary>
    public enum PeripheralType
    {
        [DescriptionLocalized("PeripheralType_Weight", typeof(GlobalStrings))]
        Weight = 1,

        [DescriptionLocalized("PeripheralType_BloodPressure", typeof(GlobalStrings))]
        BloodPressure,

        [DescriptionLocalized("PeripheralType_PulseOx", typeof(GlobalStrings))]
        PulseOx,

        [DescriptionLocalized("PeripheralType_BloodGlucose", typeof(GlobalStrings))]
        BloodGlucose,

        [DescriptionLocalized("PeripheralType_PeakFlow", typeof(GlobalStrings))]
        PeakFlow,

        [DescriptionLocalized("PeripheralType_Temperature", typeof(GlobalStrings))]
        Temperature,

        [DescriptionLocalized("PeripheralType_Pedometer", typeof(GlobalStrings))]
        Pedometer
    }
}