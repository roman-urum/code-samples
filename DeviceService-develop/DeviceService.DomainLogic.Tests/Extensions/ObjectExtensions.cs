using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DeviceService.DomainLogic.Tests.Extensions
{
    /// <summary>
    /// Extensions for object class instances.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns value of property of target object by property name.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue(this object target, string propertyName)
        {
            Type targeType = target.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(targeType.GetProperties());
            var property = props.FirstOrDefault(p => p.Name.Equals(propertyName));

            return property == null ? null : property.GetValue(target);
        }
    }
}
