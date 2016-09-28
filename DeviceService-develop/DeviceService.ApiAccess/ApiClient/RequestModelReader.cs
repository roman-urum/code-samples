using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DeviceService.Common.ApiClient;
using RestSharp;

namespace DeviceService.ApiAccess.ApiClient
{
    using Newtonsoft.Json;

    using RestSharp.Extensions;

    /// <summary>
    /// Helper to read info about request parameters
    /// from request model.
    /// Can be mapped to interface if different workflow is possible.
    /// </summary>
    internal class RequestModelReader
    {
        private static readonly IDictionary<RequestParameterType, ParameterType> RestSharpParameterTypesMap = new Dictionary
            <RequestParameterType, ParameterType>
        {
            {RequestParameterType.RequestBody, ParameterType.GetOrPost},
            {RequestParameterType.UrlSegment, ParameterType.UrlSegment},
            {RequestParameterType.QueryString, ParameterType.QueryString}
        };

        private readonly object _model;

        public RequestModelReader(object model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            _model = model;
        }

        /// <summary>
        /// Identifies if all or part of data need to be send in json.
        /// </summary>
        /// <returns></returns>
        public bool IsJsonRequest()
        {
            var attribute = _model.GetType().GetAttribute<JsonObjectAttribute>();

            return attribute != null;
        }

        /// <summary>
        /// Reads request parameters info from model.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RequestParameterModel> ReadParameters()
        {
            return _model.GetType().GetProperties()
                .Where(p => p.IsDefined(typeof(RequestParameterAttribute), false))
                .Select(p => new RequestParameterModel
                {
                    Name = this.GetParameterName(p),
                    Value = this.GetPropertyValue(p),
                    Type = this.GetParameterType(p)
                });
        }

        /// <summary>
        /// Reads value of specified property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private string GetPropertyValue(PropertyInfo property)
        {
            var value = property.GetGetMethod().Invoke(_model, null);

            return value == null ? string.Empty : value.ToString();
        }

        /// <summary>
        /// Reads ParameterType from property using RequestParameterAttribute.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private ParameterType GetParameterType(PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<RequestParameterAttribute>();

            if (attribute == null)
            {
                throw new ArgumentException("Property should has RequestParameterAttribute.");
            }

            ParameterType result;

            if (!RestSharpParameterTypesMap.TryGetValue(attribute.ParameterType, out result))
            {
                throw new NotSupportedException("Specified RequestParameterType not supported.");
            }

            return result;
        }

        /// <summary>
        /// Reads name of request parameter from property using RequestParameterAttribute.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private string GetParameterName(PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<RequestParameterAttribute>();

            return attribute == null || string.IsNullOrEmpty(attribute.ParameterName)
                ? property.Name
                : attribute.ParameterName;
        }
    }
}
