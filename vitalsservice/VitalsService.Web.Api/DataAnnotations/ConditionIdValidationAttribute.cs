using System;
using System.ComponentModel.DataAnnotations;
using VitalsService.Domain.Enums;

namespace VitalsService.Web.Api.DataAnnotations
{
    /// <summary>
    /// ConditionIdValidation.
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class ConditionIdValidationAttribute : ValidationAttribute
    {
        private readonly string dependentProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionIdValidationAttribute"/> class.
        /// </summary>
        /// <param name="dependentProperty">The dependent property.</param>
        public ConditionIdValidationAttribute(string dependentProperty)
        {
            this.dependentProperty = dependentProperty;
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Guid? targetConditionId = (Guid?)value;

            var containerType = validationContext.ObjectInstance.GetType();
            var dependentPropertyInfo = containerType.GetProperty(dependentProperty);

            if (dependentPropertyInfo != null)
            {
                var dependentPropertyValue = dependentPropertyInfo.GetValue(validationContext.ObjectInstance, null);

                if (dependentPropertyValue != null && dependentPropertyValue is ThresholdDefaultType)
                {
                    var defaultThresholdType = (ThresholdDefaultType)dependentPropertyValue;

                    if (targetConditionId.HasValue && defaultThresholdType == ThresholdDefaultType.Customer)
                    {
                        return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
                    }

                    if (!targetConditionId.HasValue && defaultThresholdType == ThresholdDefaultType.Condition)
                    {
                        return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}