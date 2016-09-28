using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VitalsService.Domain.Enums;

namespace VitalsService.Web.Api.DataAnnotations
{
    /// <summary>
    /// ValidateUnitForVitalAttribute.
    /// </summary>
    public class ValidateUnitForVitalAttribute : ValidationAttribute
    {
        private readonly string dependentProperty;

        private readonly Dictionary<VitalType, IList<UnitType>> vitalToUnitsMap = new Dictionary<VitalType, IList<UnitType>>()
        {
            { VitalType.SystolicBloodPressure, new List<UnitType>() { UnitType.mmHg, UnitType.kPa } },
            { VitalType.DiastolicBloodPressure, new List<UnitType>() { UnitType.mmHg, UnitType.kPa } },
            { VitalType.HeartRate, new List<UnitType>() { UnitType.BpM } },
            { VitalType.Temperature, new List<UnitType>() { UnitType.C, UnitType.F } },
            { VitalType.Weight, new List<UnitType>() { UnitType.Lbs, UnitType.kg } },
            { VitalType.BodyMassIndex, new List<UnitType>() { UnitType.kg_m2 } },
            { VitalType.OxygenSaturation, new List<UnitType>() { UnitType.Percent } },
            { VitalType.BloodGlucose, new List<UnitType>() { UnitType.mgdl, UnitType.mmol_L } },
            { VitalType.PeakExpiratoryFlow, new List<UnitType>() { UnitType.L_min } },
            { VitalType.ForcedExpiratoryVolume, new List<UnitType>() { UnitType.L } },
            { VitalType.WalkingSteps, new List<UnitType>() { UnitType.steps_day } },
            { VitalType.RunningSteps, new List<UnitType>() { UnitType.steps_day } },
            { VitalType.ForcedVitalCapacity, new List<UnitType>() { UnitType.L } },
            { VitalType.FEV1_FVC, new List<UnitType>() { UnitType.Percent } }
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUnitForVitalAttribute" /> class.
        /// </summary>
        /// <param name="dependentProperty">The dependent property.</param>
        public ValidateUnitForVitalAttribute(string dependentProperty)
        {
            this.dependentProperty = dependentProperty;
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var targetValue = (UnitType)value;
            var containerType = validationContext.ObjectInstance.GetType();
            var dependentPropertyInfo = containerType.GetProperty(dependentProperty);

            if (dependentPropertyInfo != null)
            {
                var dependentPropertyValue = dependentPropertyInfo.GetValue(validationContext.ObjectInstance, null);

                if (dependentPropertyValue != null && dependentPropertyValue is VitalType)
                {
                    var vitalType = (VitalType)dependentPropertyValue;
                    IList<UnitType> allowedUnits;

                    if (vitalToUnitsMap.TryGetValue(vitalType, out allowedUnits) && !allowedUnits.Contains(targetValue))
                    {
                        return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}