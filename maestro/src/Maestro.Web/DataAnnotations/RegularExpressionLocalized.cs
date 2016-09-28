using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Maestro.Web.DataAnnotations
{
    using System.Web.Mvc;

    using Maestro.Web.Resources;

    /// <summary>
    /// Overrides default RegularExpressionAttribute to use resource strings by id.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class RegularExpressionLocalizedAttribute : RegularExpressionAttribute
    {
        static RegularExpressionLocalizedAttribute()
        {
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RegularExpressionLocalizedAttribute),
                                                                  typeof(RegularExpressionAttributeAdapter));
        }

        public RegularExpressionLocalizedAttribute(string pattern, string resourceId):base(pattern)
        {
            ErrorMessage = GlobalStrings.ResourceManager.GetString(resourceId);
        }
    }
}