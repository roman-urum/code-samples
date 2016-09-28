using System.Reflection;

namespace Maestro.Common.ApiClient
{
    /// <summary>
    /// Default converter to retrieve property value for request.
    /// </summary>
    public class RequestValueConverter : IRequestValueConverter
    {
        /// <summary>
        /// Returns value from object for specified property in default string form.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public string GetValue(object target, PropertyInfo property)
        {
            var value = property.GetGetMethod().Invoke(target, null);

            return value == null ? null : value.ToString();
        }
    }
}
