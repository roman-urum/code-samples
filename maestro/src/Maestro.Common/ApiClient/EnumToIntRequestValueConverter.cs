using System;
using System.Reflection;

namespace Maestro.Common.ApiClient
{
    /// <summary>
    /// Reads enum values as integer string.
    /// </summary>
    public class EnumToIntRequestValueConverter : IRequestValueConverter
    {
        public string GetValue(object target, PropertyInfo property)
        {
            var value = property.GetGetMethod().Invoke(target, null);

            if (value == null)
            {
                return null;
            }

            if (property.PropertyType.IsEnum)
            {
                return ((int) value).ToString();
            }
            else
            {
                throw new InvalidCastException();
            }
        }
    }
}
