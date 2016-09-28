using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using VitalsService.Domain.Enums;

namespace VitalsService.Web.Api.DataAnnotations
{
    /// <summary>
    /// ValidateMaximumValueForVitalAttribute.
    /// </summary>
    public class ValidateMaximumValueForVitalAttribute : ValidationAttribute
    {
        private readonly string dependentVitalProperty;
        private readonly string dependentUnitProperty;
        private decimal allowedMaxValue;

        private readonly Dictionary<VitalType, Dictionary<UnitType, decimal>> vitalUnitToMaxValueMap = new Dictionary<VitalType, Dictionary<UnitType, decimal>>()
        {
            { VitalType.SystolicBloodPressure, new Dictionary<UnitType, decimal>() { { UnitType.mmHg, 250 }, { UnitType.kPa, 35 } } },
            { VitalType.DiastolicBloodPressure, new Dictionary<UnitType, decimal>(){ { UnitType.mmHg, 200 }, { UnitType.kPa, 30 } } },
            { VitalType.HeartRate, new Dictionary<UnitType, decimal>() { { UnitType.BpM, 250 } } },
            { VitalType.Temperature, new Dictionary<UnitType, decimal>() { { UnitType.C, 43 }, { UnitType.F, 110 } } },
            { VitalType.Weight, new Dictionary<UnitType, decimal>() { { UnitType.Lbs, (decimal)999.9 }, { UnitType.kg, (decimal)499.9 } } },
            { VitalType.BodyMassIndex, new Dictionary<UnitType, decimal>() { { UnitType.kg_m2, 1000 } } }, // ToDo: Add MaxValue
            { VitalType.OxygenSaturation, new Dictionary<UnitType, decimal>() { { UnitType.Percent, 101 } } },
            { VitalType.BloodGlucose, new Dictionary<UnitType, decimal>() { { UnitType.mgdl, 999 }, { UnitType.mmol_L, 50 } } },
            { VitalType.PeakExpiratoryFlow, new Dictionary<UnitType, decimal>() { { UnitType.L_min, 999 } } },
            { VitalType.ForcedExpiratoryVolume, new Dictionary<UnitType, decimal>() { { UnitType.L, (decimal)9.99 } } },
            { VitalType.WalkingSteps, new Dictionary<UnitType, decimal>() { { UnitType.steps_day, 100000 } } },
            { VitalType.RunningSteps, new Dictionary<UnitType, decimal>() { { UnitType.steps_day, 100000 } } },
            { VitalType.ForcedVitalCapacity, new Dictionary<UnitType, decimal>() { { UnitType.L, 999 } } },
            { VitalType.FEV1_FVC, new Dictionary<UnitType, decimal>() { { UnitType.Percent, 101 } } }
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUnitForVitalAttribute" /> class.
        /// </summary>
        /// <param name="dependentVitalProperty">The dependent vital property.</param>
        /// <param name="dependentUnitProperty">The dependent unit property.</param>
        public ValidateMaximumValueForVitalAttribute(
            string dependentVitalProperty,
            string dependentUnitProperty
        )
        {
            this.dependentVitalProperty = dependentVitalProperty;
            this.dependentUnitProperty = dependentUnitProperty;
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var targetValue = (decimal)value;
            var containerType = validationContext.ObjectInstance.GetType();
            var dependentVitalPropertyInfo = containerType.GetProperty(dependentVitalProperty);
            var dependentUnitPropertyInfo = containerType.GetProperty(dependentUnitProperty);

            if (dependentVitalPropertyInfo != null && dependentUnitPropertyInfo != null)
            {
                var dependentVitalPropertyValue = dependentVitalPropertyInfo.GetValue(validationContext.ObjectInstance, null);
                var dependentUnitPropertyValue = dependentUnitPropertyInfo.GetValue(validationContext.ObjectInstance, null);

                if (dependentVitalPropertyValue != null && dependentVitalPropertyValue is VitalType)
                {
                    var vitalType = (VitalType)dependentVitalPropertyValue;

                    Dictionary<UnitType, decimal> unitTypeAllowedMaximumValues;

                    if (vitalUnitToMaxValueMap.TryGetValue(vitalType, out unitTypeAllowedMaximumValues) &&
                        unitTypeAllowedMaximumValues.TryGetValue((UnitType)dependentUnitPropertyValue, out this.allowedMaxValue) &&
                        targetValue > allowedMaxValue)
                    {
                        return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
                    }
                }
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Formats the error message.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, this.ErrorMessageString, name, this.allowedMaxValue);
        }
    }
}