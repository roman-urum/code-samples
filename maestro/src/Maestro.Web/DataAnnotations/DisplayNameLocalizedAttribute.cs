using System;
using System.ComponentModel;
using Maestro.Web.Resources;

namespace Maestro.Web.DataAnnotations
{
    /// <summary>
    /// Overrides default DisplayNameAttribute to use resource strings by id.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class DisplayNameLocalizedAttribute : DisplayNameAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayNameLocalizedAttribute"/> class.
        /// </summary>
        /// <param name="resourceId">The resource identifier.</param>
        public DisplayNameLocalizedAttribute(string resourceId)
            : base(GlobalStrings.ResourceManager.GetString(resourceId))
        {
        }
    }
}