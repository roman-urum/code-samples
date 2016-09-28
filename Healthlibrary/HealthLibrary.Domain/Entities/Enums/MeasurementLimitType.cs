namespace HealthLibrary.Domain.Entities.Enums
{
    /// <summary>
    /// MeasurementLimitType.
    /// </summary>
    public enum MeasurementLimitType
    {
        PatientBloodGlucoseHigherLimit = 1,
        PatientBloodGlucoseLowerLimit,

        PatientOxygenSaturationHigherLimit,
        PatientOxygenSaturationLowerLimit,

        PatientBloodPressureHigherLimit,
        PatientBloodPressureLowerLimit,

        PatientHeartRateHigherLimit,
        PatientHeartRateLowerLimit,

        PatientTemperatureHigherLimit,
        PatientTemperatureLowerLimit,

        PatientForcedExpiratoryVolumeHigherLimit,
        PatientForcedExpiratoryVolumeLowerLimit,

        PatientPeakExpiratoryFlowHigherLimit,
        PatientPeakExpiratoryFlowLowerLimit,

        PatientWeightHigherLimit,
        PatientWeightLowerLimit,

        PatientWalkingStepsHigherLimit,
        PatientWalkingStepsLowerLimit,

        PatientRunningStepsHigherLimit,
        PatientRunningStepsLowerLimit,

        PatientForcedVitalCapacityHigherLimit,
        PatientForcedVitalCapacityLowerLimit,

        PatientFEV1_FVCHigherLimit,
        PatientFEV1_FVCLowerLimit
    }
}