using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Maestro.Common.ApiClient;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;

namespace Maestro.DataAccess.Api.ApiClient
{
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
        private static readonly IRequestValueConverter DefaultRequestValueConverter = new RequestValueConverter();

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

            return attribute != null || _model is IList;
        }

        /// <summary>
        /// Reads request parameters info from model.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RequestParameterModel> ReadParameters()
        {
            var requestProperties = _model.GetType().GetProperties()
                .Where(p => p.IsDefined(typeof (RequestParameterAttribute), false));
            var result = new List<RequestParameterModel>();

            foreach (var requestProperty in requestProperties)
            {
                result.AddRange(this.GetParameterModels(requestProperty));
            }

            return result;
        }

        /// <summary>
        /// Generates request parameters for specified model property.
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        private IEnumerable<RequestParameterModel> GetParameterModels(PropertyInfo propertyInfo)
        {
            var result = new List<RequestParameterModel>();

            if (typeof (IEnumerable).IsAssignableFrom(propertyInfo.PropertyType) &&
                propertyInfo.PropertyType != typeof (string))
            {
                IList listValues = propertyInfo.GetValue(_model, null) as IList;

                if (listValues != null)
                {
                    foreach (var value in listValues)
                    {
                        result.Add(new RequestParameterModel
                        {
                            Name = this.GetParameterName(propertyInfo),
                            Value = value.ToString(),
                            Type = this.GetParameterType(propertyInfo)
                        });
                    }
                }
            }
            else
            {
                result.Add(new RequestParameterModel
                {
                    Name = this.GetParameterName(propertyInfo),
                    Value = this.GetPropertyValue(propertyInfo),
                    Type = this.GetParameterType(propertyInfo)
                });
            }

            return result.Where(m => !string.IsNullOrEmpty(m.Value));
        }


        /// <summary>
        /// Reads value of specified property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private string GetPropertyValue(PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<RequestParameterAttribute>();

            if (attribute == null || attribute.ValueConverter == null)
            {
                return DefaultRequestValueConverter.GetValue(_model, property);
            }

            var customConverter = Activator.CreateInstance(attribute.ValueConverter) as IRequestValueConverter;

            if (customConverter != null)
            {
                return customConverter.GetValue(_model, property);
            }

            throw new InvalidCastException("RequestValueConverters must implement IRequestValueConverter");
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
