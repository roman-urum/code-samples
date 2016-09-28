using System.Collections.Generic;

namespace Maestro.Web.DataAnnotations
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Resources;

    /// <summary>
    /// Overrides default Range attribute to use resource strings by id
    /// </summary>
    public class RangeLocalizedAttribute : RangeAttribute, IClientValidatable
    {
        static RangeLocalizedAttribute()
        {
            //DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof (RangeLocalizedAttribute),
            //    typeof (RangeAttribute));
        }

        public RangeLocalizedAttribute(int minimum, int maximum, string resourceId) : base(minimum, maximum)
        {
            ErrorMessage = GlobalStrings.ResourceManager.GetString(resourceId);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessage,
                ValidationType = "range"
            };

            rule.ValidationParameters.Add("min", Minimum);
            rule.ValidationParameters.Add("max", Maximum);

            yield return rule;
        }
    }
}