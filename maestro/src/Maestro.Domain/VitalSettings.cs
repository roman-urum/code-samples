using System.Collections.Generic;
using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Domain
{
    /// <summary>
    /// VitalSettings
    /// </summary>
    public class VitalSettings
    {
        /// <summary>
        /// Gets the default vital units.
        /// </summary>
        /// <value>
        /// The default vital units.
        /// </value>
        public static Dictionary<VitalType, UnitType> DefaultVitalUnits { get; } = 
            new Dictionary<VitalType, UnitType>()
            {
                { VitalType.SystolicBloodPressure, UnitType.mmHg },
                { VitalType.DiastolicBloodPressure, UnitType.mmHg },
                { VitalType.HeartRate, UnitType.BpM },
                { VitalType.Temperature, UnitType.F },
                { VitalType.Weight, UnitType.Lbs },
                { VitalType.BodyMassIndex, UnitType.kg_m2 },
                { VitalType.OxygenSaturation, UnitType.Percent },
                { VitalType.BloodGlucose, UnitType.mgdl },
                { VitalType.PeakExpiratoryFlow, UnitType.L_min },
                { VitalType.ForcedExpiratoryVolume, UnitType.L },
                { VitalType.WalkingSteps, UnitType.steps_day },
                { VitalType.RunningSteps, UnitType.steps_day },
                { VitalType.ForcedVitalCapacity, UnitType.L },
                { VitalType.FEV1_FVC, UnitType.Percent }
            };

        /// <summary>
        /// Gets or sets the type of the systolic blood pressure unit.
        /// </summary>
        /// <value>
        /// The type of the systolic blood pressure unit.
        /// </value>
        public UnitType? SystolicBloodPressureUnitType { get; set; }

        /// <summary>
        /// Gets or sets the diastolic blood pressure.
        /// </summary>
        /// <value>
        /// The diastolic blood pressure.
        /// </value>
        public UnitType? DiastolicBloodPressure { get; set; }

        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        /// <value>
        /// The temperature.
        /// </value>
        public UnitType? Temperature { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public UnitType? Weight { get; set; }

        /// <summary>
        /// Gets or sets the index of the body mass.
        /// </summary>
        /// <value>
        /// The index of the body mass.
        /// </value>
        public UnitType? BodyMassIndex { get; set; }

        /// <summary>
        /// Gets or sets the oxygen saturation.
        /// </summary>
        /// <value>
        /// The oxygen saturation.
        /// </value>
        public UnitType? OxygenSaturation { get; set; }

        /// <summary>
        /// Gets or sets the blood glucose.
        /// </summary>
        /// <value>
        /// The blood glucose.
        /// </value>
        public UnitType? BloodGlucose { get; set; }

        /// <summary>
        /// Gets or sets the peak expiratory flow.
        /// </summary>
        /// <value>
        /// The peak expiratory flow.
        /// </value>
        public UnitType? PeakExpiratoryFlow { get; set; }

        /// <summary>
        /// Gets or sets the forced expiratory volume.
        /// </summary>
        /// <value>
        /// The forced expiratory volume.
        /// </value>
        public UnitType? ForcedExpiratoryVolume { get; set; }

        /// <summary>
        /// Gets or sets the walking steps.
        /// </summary>
        /// <value>
        /// The walking steps.
        /// </value>
        public UnitType? WalkingSteps { get; set; }

        /// <summary>
        /// Gets or sets the running steps.
        /// </summary>
        /// <value>
        /// The running steps.
        /// </value>
        public UnitType? RunningSteps { get; set; }

        /// <summary>
        /// Gets or sets the forced vital capacity.
        /// </summary>
        /// <value>
        /// The forced vital capacity.
        /// </value>
        public UnitType? ForcedVitalCapacity { get; set; }

        /// <summary>
        /// Gets or sets the fe V1_ FVC.
        /// </summary>
        /// <value>
        /// The fe V1_ FVC.
        /// </value>
        public UnitType? FEV1_FVC { get; set; }
    }
}