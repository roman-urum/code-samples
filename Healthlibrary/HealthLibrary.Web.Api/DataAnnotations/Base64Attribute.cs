using System;
using System.ComponentModel.DataAnnotations;

namespace HealthLibrary.Web.Api.DataAnnotations
{
    /// <summary>
    /// Validates if string provided in base-64 format.
    /// </summary>
    public class Base64Attribute : ValidationAttribute
    {
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