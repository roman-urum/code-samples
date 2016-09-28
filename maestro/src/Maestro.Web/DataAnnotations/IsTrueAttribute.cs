using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Maestro.Web.Resources;

namespace Maestro.Web.DataAnnotations
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Should be applied to fields which need to have true value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IsTrueAttribute:ValidationAttribute, IClientValidatable
    {
        public IsTrueAttribute(string messageId)
        {
            if (string.IsNullOrEmpty(messageId))
            {
                throw new ArgumentNullException("messageId");
            }

            ErrorMessage = GlobalStrings.ResourceManager.GetString(messageId);
        }

        public override bool IsValid(object value)
        {
            return value is bool && (bool)value;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
            ControllerContext context)
        {
            var rule = new ModelClientIsTrueValidationRule(ErrorMessage);

            yield return rule;
        }
    }

    /// <summary>
    /// Determines client side validation parameters.
    /// </summary>
    public class ModelClientIsTrueValidationRule : ModelClientValidationRule
    {
        public ModelClientIsTrueValidationRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "istrue";
        }
    }
}