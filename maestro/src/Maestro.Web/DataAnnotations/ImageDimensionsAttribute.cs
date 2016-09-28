using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Web;
using System.Web.Mvc;
using Maestro.Web.Resources;

namespace Maestro.Web.DataAnnotations
{
    /// <summary>
    /// Attribute to validate dimensions of uploaded image.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ImageDimensionsAttribute : ValidationAttribute, IClientValidatable
    {
        public int MaxHeight { get; set; }

        public int MaxWidth { get; set; }

        public ImageDimensionsAttribute(string messageId)
        {
            ErrorMessage = GlobalStrings.ResourceManager.GetString(messageId);
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;

            if (file == null)
            {
                return true;
            }

            var image = Image.FromStream(file.InputStream);

            return MaxHeight >= image.Height && MaxWidth >= image.Width;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
            ControllerContext context)
        {
            var rule = new ModelClientFileExtensionValidationRule(ErrorMessage, this.MaxHeight, this.MaxWidth);

            yield return rule;
        }

        /// <summary>
        /// Determines client side validation parameters.
        /// </summary>
        public class ModelClientFileExtensionValidationRule : ModelClientValidationRule
        {
            public ModelClientFileExtensionValidationRule(string errorMessage, int maxHeight, int maxWidth)
            {
                ErrorMessage = errorMessage;
                ValidationType = "imagedimensions";

                if (maxHeight > 0)
                {
                    ValidationParameters.Add("maxheight", maxHeight);
                }

                if (maxWidth > 0)
                {
                    ValidationParameters.Add("maxwidth", maxWidth);
                }
            }
        }

        
    }
}