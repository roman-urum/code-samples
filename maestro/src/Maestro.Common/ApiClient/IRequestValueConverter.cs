using System.Reflection;

namespace Maestro.Common.ApiClient
{
    /// <summary>
    /// Interface to convert values of object properties
    /// as it is needed in request.
    /// </summary>
    public interface IRequestValueConverter
    {
        /// <summary>
        /// Returns value from object for specified property in default string form.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        string GetValue(object target, PropertyInfo property);
    }
}
