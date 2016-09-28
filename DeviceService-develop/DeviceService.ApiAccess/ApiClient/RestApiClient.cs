using System;
using System.Net;
using System.Threading.Tasks;
using DeviceService.ApiAccess.Extensions;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using DeviceService.Common.Exceptions;
using Newtonsoft.Json;

namespace DeviceService.ApiAccess.ApiClient
{
    public class RestApiClient : IRestApiClient
    {
        private static readonly IEnumerable<HttpStatusCode> ValidStatusCodes = new List<HttpStatusCode>
        {
            HttpStatusCode.OK,
            HttpStatusCode.Created,
            HttpStatusCode.NoContent
        };

        private readonly IRestClient _restClient;

        public RestApiClient(string apiUrl)
        {
            _restClient = new RestClient(apiUrl);
        }

        /// <summary>
        /// Send request to specified endpoint.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="model"></param>
        /// <param name="method"></param>
        /// <param name="isAuthorized">Identifies if request should includes authorization token.</param>
        /// <returns></returns>
        public TResponse SendRequest<TResponse>(string endpoint, object model, Method method, string authToken = null)
            where TResponse : class, new()
        {
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentNullException("endpoint");
            }

            IRestRequest request = this.InitRequest(endpoint, model, method, authToken, null);
            var result = _restClient.Execute<TResponse>(request);

            this.VerifyResponse(result);

            return result.GetData();
        }

        /// <summary>
        /// Send request to specified endpoint async.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="model"></param>
        /// <param name="method"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<TResponse> SendRequestAsync<TResponse>(string endpoint, object model, Method method,
            Dictionary<string, string> headers) where TResponse : class, new()
        {
            return await this.SendRequestAsync<TResponse>(endpoint, model, method, null, headers);
        }

        /// <summary>
        /// Send request to specified endpoint async.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="model"></param>
        /// <param name="method"></param>
        /// <param name="isAuthorized">Identifies if request should includes authorization token.</param>
        /// <returns></returns>
        public async Task<TResponse> SendRequestAsync<TResponse>(string endpoint, object model, Method method,
            string authToken = null, Dictionary<string, string> headers = null) where TResponse : class, new()
        {
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentNullException("endpoint");
            }

            IRestRequest request = this.InitRequest(endpoint, model, method, authToken, headers);
            var result = await _restClient.ExecuteTaskAsync<TResponse>(request);

            this.VerifyResponse(result);

            return result.GetData();
        }

        /// <summary>
        /// Send request to specified endpoint async.
        /// (Without reading of response data).
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="model"></param>
        /// <param name="method"></param>
        /// <param name="isAuthorized">Identifies if request should includes authorization token.</param>
        /// <returns></returns>
        public async Task SendRequestAsync(string endpoint, object model, Method method, string authToken = null)
        {
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentNullException("endpoint");
            }

            IRestRequest request = this.InitRequest(endpoint, model, method, authToken, null);
            var result = await _restClient.ExecuteTaskAsync(request);

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
                throw new AuthorizationException(_restClient.BaseUrl.AbsoluteUri, "Invalid credentials.");
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ServiceNotFoundException(_restClient.BaseUrl.AbsoluteUri);
            }

            if (ValidStatusCodes.Contains(response.StatusCode))
            {
                return;
            }

            string message;

            if (string.IsNullOrEmpty(response.ErrorMessage) &&
                !string.IsNullOrEmpty(response.Content))
            {
                var responseError = JsonConvert.DeserializeObject<ErrorResponseDto>(response.Content);
                message = responseError.Message;
            }
            else
            {
                message = response.ErrorMessage;
            }

            throw new ServiceException(_restClient.BaseUrl.AbsoluteUri, response.StatusCode, message);
        }

        /// <summary>
        /// Initializes request data.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="model"></param>
        /// <param name="method"></param>
        /// <param name="headers"></param>
        /// <returns>Instance of IRestRequest</returns>
        private IRestRequest InitRequest(string endpoint, object model, Method method, string authToken, Dictionary<string, string> headers)
        {
            var request = new RestRequest(endpoint, method);

            if (!string.IsNullOrEmpty(authToken))
            {
                request.AddHeader("Authorization", string.Concat("Bearer ", authToken));
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
                throw new ArgumentNullException("restRequest");
            }

            if (headers == null)
            {
                throw new ArgumentNullException("headers");
            }

            foreach (var header in headers)
            {
                restRequest.AddHeader(header.Key, header.Value);
            }
        }
    }
}
