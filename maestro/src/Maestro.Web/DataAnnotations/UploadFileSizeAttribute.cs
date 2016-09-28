using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Maestro.Web.Resources;

namespace Maestro.Web.DataAnnotations
{
    /// <summary>
    /// Attribute to set size restriction for file (in bytes).
    /// </summary>
    public class UploadFileSizeAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly int _max;

        public UploadFileSizeAttribute(int max, string messageId)
        {
            _max = max;
            ErrorMessage = GlobalStrings.ResourceManager.GetString(messageId);
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;

            if (file == null)
            {
                return true;
            }

            return file.ContentLength <= _max;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
            ControllerContext context)
        {
            var rule = new ModelClientFileSizeValidationRule(ErrorMessage, _max);

            yield return rule;
        }
    }

    /// <summary>
    /// Determines client side validation parameters.
    /// </summary>
    public class ModelClientFileSizeValidationRule : ModelClientValidationRule
    {
        public ModelClientFileSizeValidationRule(string errorMessage, int max)
        {
            ErrorMessage = errorMessage;
            ValidationType = "filesize";
            ValidationParameters.Add("maxsize", max);
        }
    }
}