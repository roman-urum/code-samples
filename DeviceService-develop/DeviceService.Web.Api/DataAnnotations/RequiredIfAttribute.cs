using System;
using System.ComponentModel.DataAnnotations;

namespace DeviceService.Web.Api.DataAnnotations
{
    /// <summary>
    /// RequiredIfAttribute.
    /// </summary>
    public class RequiredIfAttribute : RequiredAttribute
    {
        private String PropertyName { get; set; }

        private Object DesiredValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfAttribute"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="desiredvalue">The desiredvalue.</param>
        public RequiredIfAttribute(String propertyName, Object desiredvalue)
        {
            PropertyName = propertyName;
            DesiredValue = desiredvalue;
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            Object instance = context.ObjectInstance;
            Type type = instance.GetType();
            Object proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);

            if (proprtyvalue.ToString() == DesiredValue.ToString())
            {
                ValidationResult result = base.IsValid(value, context);

                return result;
            }

            return ValidationResult.Success;
        }
    }
}