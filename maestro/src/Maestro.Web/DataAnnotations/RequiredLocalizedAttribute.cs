using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Maestro.Web.Resources;

namespace Maestro.Web.DataAnnotations
{
    /// <summary>
    /// Overrides default RequiredAttribute to use resource strings by id.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredLocalizedAttribute : RequiredAttribute
    {
        static RequiredLocalizedAttribute()
        {
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredLocalizedAttribute),
                typeof(RequiredAttributeAdapter));
        }

        public RequiredLocalizedAttribute(string resourceId)
        {
            ErrorMessage = GlobalStrings.ResourceManager.GetString(resourceId);
        }
    }
}