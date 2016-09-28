using System;

namespace HealthLibrary.Common.ApiClient
{
    /// <summary>
    /// Apply this attribute to property of dto for http request to define 
    /// at which place in request the value should be used.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequestParameterAttribute : Attribute
    {
        public RequestParameterType ParameterType { get; private set; }

        public RequestParameterAttribute(RequestParameterType parameterType)
        {
            ParameterType = parameterType;
        }
    }
}
