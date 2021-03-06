﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CustomerService.Common.ApiClient;
using RestSharp;

namespace CustomerService.ApiAccess.ApiClient
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
            {RequestParameterType.UrlSegment, ParameterType.UrlSegment}
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
        /// Reads request parameters info from model.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RequestParameterModel> ReadParameters()
        {
            return _model.GetType().GetProperties()
                .Where(p => p.IsDefined(typeof(RequestParameterAttribute), false))
                .Select(p => new RequestParameterModel
                {
                    Name = p.Name,
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
    }
}
