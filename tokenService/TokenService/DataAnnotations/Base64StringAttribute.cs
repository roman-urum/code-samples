using System;
using System.ComponentModel.DataAnnotations;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DataAnnotations
{
    /// <summary>
    /// Validates if string provided in base-64 format.
    /// </summary>
    public class Base64StringAttribute : ValidationAttribute
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        /// true if the specified value is valid; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var valueString = value.ToString();

            if (string.IsNullOrEmpty(valueString))
            {
                return false;
            }

            try
            {
                Convert.FromBase64String(valueString);
                
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}