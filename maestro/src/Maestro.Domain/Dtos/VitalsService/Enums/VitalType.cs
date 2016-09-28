using System.ComponentModel;

namespace Maestro.Domain.Dtos.VitalsService.Enums
{
    /// <summary>
    /// VitalType.
    /// </summary>
    public enum VitalType
    {
        /// <summary>
        /// The systolic blood pressure.
        /// </summary>
        [Description("Systolic Blood Pressure")]
        SystolicBloodPressure = 1,

        /// <summary>
        /// The diastolic blood pressure.
        /// </summary>
        [Description("Diastolic Blood Pressure")]
        DiastolicBloodPressure,

        /// <summary>
        /// The heart rate.
        /// </summary>
        [Description("Heart Rate")]
        HeartRate,

        /// <summary>
        /// The body temperature.
        /// </summary>
        [Description("Temperature")]
        Temperature,

        /// <summary>
        /// The weight.
        /// </summary>
        [Description("Weight")]
        Weight,

        /// <summary>
        /// The body mass index.
        /// </summary>
        [Description("Body Mass Index")]
        BodyMassIndex,

        /// <summary>
        /// The oxygen saturation.
        /// </summary>
        [Description("Oxygen Saturation")]
        OxygenSaturation,

        /// <summary>
        /// The blood glucose.
        /// </summary>
        [Description("Blood Glucose")]
        BloodGlucose,

        /// <summary>
        /// The peak expiratory flow.
        /// </summary>
        [Description("Peak Expiratory Flow")]
        PeakExpiratoryFlow,

        /// <summary>
        /// The forced expiratory volume.
        /// </summary>
        [Description("Forced Expiratory Volume")]
        ForcedExpiratoryVolume,

        /// <summary>
        /// The walking steps.
        /// </summary>
        [Description("Walking Steps")]
        WalkingSteps,

        /// <summary>
        /// The running steps.
        /// </summary>
        [Description("Running Steps")]
        RunningSteps,

        /// <summary>
        /// Forced Vital Capacity
        /// </summary>
        [Description("Forced Vital Capacity")]
        ForcedVitalCapacity,

        /// <summary>
        /// FEV1/FVC ratio
        /// </summary>
        [Description("FEV1/FVC Ratio")]
        FEV1_FVC
    }
}