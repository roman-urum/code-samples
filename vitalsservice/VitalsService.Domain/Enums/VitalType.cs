using System.ComponentModel;

namespace VitalsService.Domain.Enums
{
    /// <summary>
    /// VitalType.
    /// </summary>
    public enum VitalType
    {
        /// <summary>
        /// The systolic blood pressure.
        /// </summary>
        [Description("mmHg, kPa")]
        SystolicBloodPressure = 1,

        /// <summary>
        /// The diastolic blood pressure.
        /// </summary>
        [Description("mmHg, kPa")]
        DiastolicBloodPressure,

        /// <summary>
        /// The heart rate.
        /// </summary>
        [Description("BpM")]
        HeartRate,

        /// <summary>
        /// The body temperature.
        /// </summary>
        [Description("C, F")]
        Temperature,

        /// <summary>
        /// The weight.
        /// </summary>
        [Description("kg, Lbs")]
        Weight,

        /// <summary>
        /// The body mass index.
        /// </summary>
        [Description("kg_m2")]
        BodyMassIndex,

        /// <summary>
        /// The oxygen saturation.
        /// </summary>
        [Description("Percent")]
        OxygenSaturation,

        /// <summary>
        /// The blood glucose.
        /// </summary>
        [Description("mgdl, mmol_L")]
        BloodGlucose,

        /// <summary>
        /// The peak expiratory flow.
        /// </summary>
        [Description("L_min")]
        PeakExpiratoryFlow,

        /// <summary>
        /// The forced expiratory volume.
        /// </summary>
        [Description("L")]
        ForcedExpiratoryVolume,

        /// <summary>
        /// The walking steps.
        /// </summary>
        [Description("steps_day")]
        WalkingSteps,

        /// <summary>
        /// The running steps.
        /// </summary>
        [Description("steps_day")]
        RunningSteps,

        /// <summary>
        /// Forced Vital Capacity.
        /// </summary>
        [Description("L")]
        ForcedVitalCapacity,

        /// <summary>
        /// FEV1/FVC
        /// </summary>
        [Description("Percent")]
        FEV1_FVC
    }
}