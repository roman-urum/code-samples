using System.ComponentModel.DataAnnotations;

namespace VitalsService.Web.Api.Extensions
{
    /// <summary>
    /// Extension methods for ValidationContext instances.
    /// </summary>
    public static class ValidationContextExtensions
    {
        /// <summary>
        /// Reads value from validation context by property name.
        /// </summary>
        /// <param name="validationContext"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object ReadPropertyValue(this ValidationContext validationContext, string propertyName)
        {
            var property = validationContext.ObjectType.GetProperty(propertyName);

            if (property == null)
            {
                return null;
            }

            return property.GetValue(validationContext.ObjectInstance);
        }
    }
}