using System;
using System.ComponentModel;
using System.Resources;

namespace CustomerService.Common.CustomAttributes
{
    /// <summary>
    /// DescriptionLocalizedAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class DescriptionLocalizedAttribute : DescriptionAttribute
    {
        private readonly string resourceKey;
        private readonly ResourceManager resource;

        /// <summary>
        /// Initializes a new instance of the <see cref="DescriptionLocalizedAttribute"/> class.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="resourceType">Type of the resource.</param>
        public DescriptionLocalizedAttribute(string resourceKey, Type resourceType)
        {
            this.resource = new ResourceManager(resourceType);
            this.resourceKey = resourceKey;
        }

        /// <summary>
        /// Gets the description stored in this attribute.
        /// </summary>
        public override string Description
        {
            get
            {
                string displayName = resource.GetString(resourceKey);

                return string.IsNullOrEmpty(displayName)
                    ? string.Format("[[{0}]]", resourceKey)
                    : displayName;
            }
        }
    }
}