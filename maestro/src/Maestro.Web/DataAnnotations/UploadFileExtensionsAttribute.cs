using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maestro.Web.Resources;

namespace Maestro.Web.DataAnnotations
{
    /// <summary>
    /// Attribute to specify extension restrictions for files.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UploadFileExtensionsAttribute : ValidationAttribute, IClientValidatable
    {
        private List<string> ValidExtensions { get; set; }

        public UploadFileExtensionsAttribute(string fileExtensions, string messageId)
        {
            ValidExtensions = fileExtensions.Split('|').ToList();
            ErrorMessage = GlobalStrings.ResourceManager.GetString(messageId);
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;

            if (file == null)
            {
                return true;    
            }

            var fileName = file.FileName;
            var isValidExtension = ValidExtensions.Any(y => fileName.EndsWith(y));

            return isValidExtension;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
            ControllerContext context)
        {
            var rule = new ModelClientFileExtensionValidationRule(ErrorMessage, ValidExtensions);

            yield return rule;
        }
    }

    /// <summary>
    /// Determines client side validation parameters.
    /// </summary>
    public class ModelClientFileExtensionValidationRule : ModelClientValidationRule
    {
        public ModelClientFileExtensionValidationRule(string errorMessage, IEnumerable<string> fileExtensions)
        {
            ErrorMessage = errorMessage;
            ValidationType = "fileextensions";
            ValidationParameters.Add("fileextensions", string.Join(",", fileExtensions));
        }
    }
}