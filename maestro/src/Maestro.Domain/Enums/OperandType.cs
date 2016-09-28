namespace Maestro.Domain.Enums
{
    /// <summary>
    /// OperandTypeEnum.
    /// </summary>
    public enum OperandType
    {
        BloodGlucose = 1,
        OxygenSaturation,
        BloodPressure,
        HeartRate,
        Temperature,
        ForcedExpiratoryVolume,
        PeakExpiratoryFlow,
        Weight,

        SelectionAnswerChoice,
        ScaleAnswerSet,

        WalkingSteps,
        RunningSteps,

        ForcedVitalCapacity,
        FEV1_FVC
    }
}