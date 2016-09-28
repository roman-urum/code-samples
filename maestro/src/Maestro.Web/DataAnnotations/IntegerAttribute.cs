using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Maestro.Web.Resources;

namespace Maestro.Web.DataAnnotations
{
    /// <summary>
    /// Attribute to validate type format for integer properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IntegerAttribute : ValidationAttribute, IClientValidatable
    {
        public IntegerAttribute(string messageId)
        {
            if (string.IsNullOrEmpty(messageId))
            {
                throw new ArgumentNullException("messageId");
            }

            ErrorMessage = GlobalStrings.ResourceManager.GetString(messageId);
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            int result;

            return int.TryParse(value.ToString(), out result);
        }
        
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientIntegerValidationRule(ErrorMessage);

            yield return rule;
        }
    }

    /// <summary>
    /// Determines client side validation parameters.
    /// </summary>
    public class ModelClientIntegerValidationRule : ModelClientValidationRule
    {
        public ModelClientIntegerValidationRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "integer";
        }
    }
}