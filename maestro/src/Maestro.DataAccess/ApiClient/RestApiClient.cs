using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Maestro.Common.Exceptions;
using Maestro.Common.Helpers.JsonNet;
using Newtonsoft.Json;
using RestSharp;

namespace Maestro.DataAccess.Api.ApiClient
{
    /// <summary>
    /// RestApiClient.
    /// </summary>
    public class RestApiClient : IRestApiClient
    {
        private static readonly IEnumerable<HttpStatusCode> ValidStatusCodes = new List<HttpStatusCode>
        {
            HttpStatusCode.OK,
            HttpStatusCode.Created,
            HttpStatusCode.NoContent
        };

        private readonly IRestClient restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestApiClient"/> class.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        public RestApiClient(string apiUrl)
        {
            this.restClient = new RestClient(apiUrl);
        }

        /// <summary>
        /// Send request to specified endpoint.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="model">The model.</param>
        /// <param name="method">The method.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">endpoint</exception>
        public TResponse SendRequest<TResponse>(
            string endpoint,
            object model,
            Method method, 
            Dictionary<string, string> headers = null,
            string bearerToken = null
        ) where TResponse : class, new()
        {
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentNullException("endpoint");
            }

            IRestRequest request = this.InitRequest(endpoint, model, method, headers, bearerToken);
            var result = restClient.Execute<TResponse>(request);
            
            this.VerifyResponse(result);

            return JsonConvert.DeserializeObject<TResponse>(result.Content, new KnownTypeConverter());
        }

        /// <summary>
        /// Send request to specified endpoint async.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="model">The model.</param>
        /// <param name="method">The method.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">endpoint</exception>
        public async Task<TResponse> SendRequestAsync<TResponse>(
            string endpoint,
            object model,
            Method method,
            Dictionary<string, string> headers = null,
            string bearerToken = null
        ) where TResponse : class, new()
        {
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentNullException("endpoint");
            }

            IRestRequest request = this.InitRequest(endpoint, model, method, headers, bearerToken);
            var result = await restClient.ExecuteTaskAsync<TResponse>(request);

            this.VerifyResponse(result);

            return JsonConvert.DeserializeObject<TResponse>(result.Content, new KnownTypeConverter());
        }

        /// <summary>
        /// Send request to specified endpoint async.
        /// (Without reading of response data).
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="model">The model.</param>
        /// <param name="method">The method.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">endpoint</exception>
        public async Task SendRequestAsync(
            string endpoint,
            object model,
            Method method,
            Dictionary<string, string> headers = null,
            string bearerToken = null
        )
        {
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentNullException("endpoint");
            }

            IRestRequest request = this.InitRequest(endpoint, model, method, headers, bearerToken);
            var result = await restClient.ExecuteTaskAsync(request);

            this.VerifyResponse(result);
        }

        /// <summary>
        /// Verifies if request completed correct.
        /// Raises exception if external service returns error.
        /// </summary>
        private void VerifyResponse(IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var responseError = JsonConvert.DeserializeObject<ErrorResponseDto>(response.Content);

                throw new AuthorizationException(restClient.BaseUrl.AbsoluteUri, "Invalid credentials.",
                    responseError.Error);
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ServiceNotFoundException(restClient.BaseUrl.AbsoluteUri);
            }

            if (ValidStatusCodes.Contains(response.StatusCode))
            {
                return;
            }

            if (string.IsNullOrEmpty(response.ErrorMessage) &&
                !string.IsNullOrEmpty(response.Content))
            {
                var responseError = JsonConvert.DeserializeObject<ErrorResponseDto>(response.Content);
                var message = !string.IsNullOrEmpty(responseError.Details) ? responseError.Details : responseError.Message;

                throw new ServiceException(restClient.BaseUrl.AbsoluteUri, response.StatusCode, message, responseError.Error);
            }

            throw new ServiceException(restClient.BaseUrl.AbsoluteUri, response.StatusCode, response.ErrorMessage);
        }

        /// <summary>
        /// Initializes request data.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="model">The model.</param>
        /// <param name="method">The method.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns>
        /// Instance of IRestRequest
        /// </returns>
        private IRestRequest InitRequest(
            string endpoint,
            object model, 
            Method method,
            Dictionary<string, string> headers,
            string bearerToken
        )
        {
            var request = new RestRequest(endpoint, method);

            if (!string.IsNullOrEmpty(bearerToken))
            {
                this.IncludeAuthHeader(request, bearerToken);
            }

            if (headers != null)
            {
                this.IncludeAdditionalHeaders(request, headers);
            }

            if (model == null)
            {
                return request;
            }

            var modelReader = new RequestModelReader(model);

            if (modelReader.IsJsonRequest())
            {
                request.AddParameter("application/json; charset=utf-8", 
                    JsonConvert.SerializeObject(model),
                    ParameterType.RequestBody);
            }

            foreach (var parameterModel in modelReader.ReadParameters())
            {
                request.AddParameter(parameterModel.Name, parameterModel.Value, parameterModel.Type);
            }

            return request;
        }

        /// <summary>
        /// Includes authentication header in request.
        /// </summary>
        /// <param name="restRequest">The rest request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <exception cref="AuthorizationException">Authorization token is not defined.</exception>
        private void IncludeAuthHeader(IRestRequest restRequest, string bearerToken)
        {
            if (string.IsNullOrEmpty(bearerToken))
            {
                throw new AuthorizationException(restClient.BaseUrl.AbsoluteUri, "Authorization token is not defined.", null);
            }

            restRequest.AddHeader("Authorization", string.Concat("Bearer ", bearerToken));
        }

        /// <summary>
        /// Includes the additional headers.
        /// </summary>
        /// <param name="restRequest">The rest request.</param>
        /// <param name="headers">The headers.</param>
        /// <exception cref="System.ArgumentException">Invalid argument provided.;restRequest
        /// or
        /// Invalid argument provided.;headers</exception>
        private void IncludeAdditionalHeaders(RestRequest restRequest, Dictionary<string, string> headers)
        {
            if (restRequest == null)
            {
                throw new ArgumentException("Invalid argument provided.", "restRequest");
            }

            if (headers == null)
            {
                throw new ArgumentException("Invalid argument provided.", "headers");
            }

            foreach (var header in headers)
            {
                restRequest.AddHeader(header.Key, header.Value);
            }
        }
    }
}