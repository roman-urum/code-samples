using System;
using Maestro.Common.Extensions;
using Maestro.Domain;
using Maestro.Domain.Dtos.VitalsService.Enums;
using Maestro.DomainLogic.Services.Interfaces;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// VitalConverter.
    /// </summary>
    /// <seealso cref="Maestro.DomainLogic.Services.Interfaces.IVitalConverter" />
    public class VitalConverter : IVitalConverter
    {
        /// <summary>
        /// Converts the specified source vitals to the same unit types accordding to the settings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="settings">The settings.</param>
        public void Convert<T>(T target, VitalSettings settings) where T : IConvertibleVital
        {
            ConvertVitalWithPressureUnits(target, settings);
            ConvertVitalWithTemperatureUnits(target, settings);
            ConvertVitalWithWeightUnits(target, settings);
            ConvertVitalWithConcentrationUnits(target, settings);
        }

        private void ConvertVitalWithPressureUnits<T>(T target, VitalSettings settings) where T : IConvertibleVital
        {
            if (target.Name == VitalType.SystolicBloodPressure)
            {
                var targetUnitType = settings.SystolicBloodPressureUnitType ??
                    VitalSettings.DefaultVitalUnits[VitalType.SystolicBloodPressure];

                if (target.Unit == targetUnitType)
                {
                    return;
                }

                ConvertUnit(target, targetUnitType);
            }

            if (target.Name == VitalType.DiastolicBloodPressure)
            {
                var targetUnitType = settings.DiastolicBloodPressure ??
                    VitalSettings.DefaultVitalUnits[VitalType.DiastolicBloodPressure];

                if (target.Unit == targetUnitType)
                {
                    return;
                }

                ConvertUnit(target, targetUnitType);
            }
        }

        private void ConvertVitalWithTemperatureUnits<T>(T target, VitalSettings settings) where T : IConvertibleVital
        {
            if (target.Name == VitalType.Temperature)
            {
                var targetUnitType = settings.Temperature ??
                    VitalSettings.DefaultVitalUnits[VitalType.Temperature];

                if (target.Unit == targetUnitType)
                {
                    return;
                }

                ConvertUnit(target, targetUnitType);
            }
        }

        private void ConvertVitalWithWeightUnits<T>(T target, VitalSettings settings) where T : IConvertibleVital
        {
            if (target.Name == VitalType.Weight)
            {
                var targetUnitType = settings.Weight ??
                    VitalSettings.DefaultVitalUnits[VitalType.Weight];

                if (target.Unit == targetUnitType)
                {
                    return;
                }

                ConvertUnit(target, targetUnitType);
            }
        }

        private void ConvertVitalWithConcentrationUnits<T>(T target, VitalSettings settings) where T : IConvertibleVital
        {
            if (target.Name == VitalType.BloodGlucose)
            {
                var targetUnitType = settings.BloodGlucose ??
                    VitalSettings.DefaultVitalUnits[VitalType.BloodGlucose];

                if (target.Unit == targetUnitType)
                {
                    return;
                }

                ConvertUnit(target, targetUnitType);
            }
        }

        private void ConvertUnit<T>(T sourceVital, UnitType targetUnitType) where T : IConvertibleVital
        {
            if (targetUnitType == UnitType.mmHg)
            {
                if (sourceVital.Unit != UnitType.kPa)
                {
                    ThrowInvalidOperationException(sourceVital.Unit, targetUnitType);
                }

                sourceVital.Value = Math.Round(sourceVital.Value * (decimal)7.50061561303, 2);
            }

            if (targetUnitType == UnitType.kPa)
            {
                if (sourceVital.Unit != UnitType.mmHg)
                {
                    ThrowInvalidOperationException(sourceVital.Unit, targetUnitType);
                }

                sourceVital.Value = Math.Round(sourceVital.Value * (decimal)0.13332239, 2);
            }

            if (targetUnitType == UnitType.Lbs)
            {
                if (sourceVital.Unit != UnitType.kg)
                {
                    ThrowInvalidOperationException(sourceVital.Unit, targetUnitType);
                }

                sourceVital.Value = Math.Round(sourceVital.Value * (decimal)2.20462262185, 2);
            }

            if (targetUnitType == UnitType.kg)
            {
                if (sourceVital.Unit != UnitType.Lbs)
                {
                    ThrowInvalidOperationException(sourceVital.Unit, targetUnitType);
                }

                sourceVital.Value = Math.Round(sourceVital.Value * (decimal)0.45359237, 2);
            }

            if (targetUnitType == UnitType.C)
            {
                if (sourceVital.Unit != UnitType.F)
                {
                    ThrowInvalidOperationException(sourceVital.Unit, targetUnitType);
                }

                sourceVital.Value = Math.Round((sourceVital.Value - 32) / (decimal)1.8, 2);
            }

            if (targetUnitType == UnitType.F)
            {
                if (sourceVital.Unit != UnitType.C)
                {
                    ThrowInvalidOperationException(sourceVital.Unit, targetUnitType);
                }

                sourceVital.Value = Math.Round(sourceVital.Value * (decimal)1.8 + 32, 2);
            }

            if (targetUnitType == UnitType.mgdl)
            {
                if (sourceVital.Unit != UnitType.mmol_L)
                {
                    ThrowInvalidOperationException(sourceVital.Unit, targetUnitType);
                }

                sourceVital.Value = Math.Round(sourceVital.Value * 18, 2);
            }

            if (targetUnitType == UnitType.mmol_L)
            {
                if (sourceVital.Unit != UnitType.mgdl)
                {
                    ThrowInvalidOperationException(sourceVital.Unit, targetUnitType);
                }

                sourceVital.Value = Math.Round(sourceVital.Value / 18, 2);
            }

            sourceVital.Unit = targetUnitType;
        }

        private void ThrowInvalidOperationException(UnitType sourceUnitType, UnitType targetUnitType)
        {
            throw new InvalidOperationException(string.Format("{0} unit could not be converted to {1}", sourceUnitType.Description(), targetUnitType.Description()));
        }
    }
}