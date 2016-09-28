using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Extensions;
using VitalsService.Web.Api.Models.HealthSessions;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.DataAnnotations
{
    /// <summary>
    /// Verifies that element value type matches to element type.
    /// Applies to elements collection.
    /// </summary>
    public class ElementValueTypeValidationAttribute : ValidationAttribute
    {
        private static readonly IDictionary<HealthSessionElementType, IList<HealthSessionElementValueType>>
            ElementsToValuesMap = new Dictionary<HealthSessionElementType, IList<HealthSessionElementValueType>>
            {
                {
                    HealthSessionElementType.Measurement, new List<HealthSessionElementValueType>
                    {
                        HealthSessionElementValueType.MeasurementAnswer
                    }
                },
                {
                    HealthSessionElementType.Question, new List<HealthSessionElementValueType>
                    {
                        HealthSessionElementValueType.SelectionAnswer,
                        HealthSessionElementValueType.OpenEndedAnswer,
                        HealthSessionElementValueType.ScaleAnswer
                    }
                },
                {
                    HealthSessionElementType.TextMedia, new List<HealthSessionElementValueType>()
                },
                {
                    HealthSessionElementType.Assessment, new List<HealthSessionElementValueType>
                    {
                        HealthSessionElementValueType.StethoscopeAnswer
                    }
                }
            };

        private readonly string typeFieldName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementValueTypeValidationAttribute"/> class.
        /// </summary>
        /// <param name="typeFieldName">Name of the type field.</param>
        /// <exception cref="System.ArgumentNullException">typeFieldName</exception>
        public ElementValueTypeValidationAttribute(string typeFieldName)
        {
            if (string.IsNullOrEmpty(typeFieldName))
            {
                throw new ArgumentNullException("typeFieldName");
            }

            this.typeFieldName = typeFieldName;
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var typeValue = validationContext.ReadPropertyValue(typeFieldName);
            HealthSessionElementType actualType;

            if (typeValue == null || !Enum.TryParse(typeValue.ToString(), out actualType) ||
                !ElementsToValuesMap.ContainsKey(actualType))
            {
                return ValidationResult.Success;
            }

            var supportedValuesTypes = ElementsToValuesMap[actualType];
            var elementValues = value as IList<HealthSessionElementValueDto>;

            if (elementValues == null)
            {
                return ValidationResult.Success;
            }

            foreach (var elementValue in elementValues)
            {
                if (!supportedValuesTypes.Contains(elementValue.Type))
                {
                    return new ValidationResult(GlobalStrings.ElementValueTypeValidationAttribute_ValidationError);
                }
            }

            return ValidationResult.Success;
        }
    }
}