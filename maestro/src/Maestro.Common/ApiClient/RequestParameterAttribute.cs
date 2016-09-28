using System;

namespace Maestro.Common.ApiClient
{
    /// <summary>
    /// Apply this attribute to property of dto for http request to define 
    /// at which place in request the value should be used.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequestParameterAttribute : Attribute
    {
        /// <summary>
        /// Values converter which should be used to get value for request from property.
        /// </summary>
        public Type ValueConverter { get; set; }

        public RequestParameterType ParameterType { get; private set; }

        public string ParameterName { get; private set; }

        public RequestParameterAttribute(RequestParameterType parameterType, string parameterName = null)
        {
            ParameterType = parameterType;
            ParameterName = parameterName;
        }
    }
}
