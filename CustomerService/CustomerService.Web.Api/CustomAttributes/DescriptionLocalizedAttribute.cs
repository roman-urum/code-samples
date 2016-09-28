using System;
using System.ComponentModel;
using CustomerService.Web.Api.Resources;

namespace CustomerService.Web.Api.CustomAttributes
{
    /// <summary>
    /// DescriptionLocalizedAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class DescriptionLocalizedAttribute : DescriptionAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DescriptionLocalizedAttribute"/> class.
        /// </summary>
        /// <param name="resourceId">The resource identifier.</param>
        public DescriptionLocalizedAttribute(string resourceId) :
            base(GlobalStrings.ResourceManager.GetString(resourceId))
        {
        }
    }
}