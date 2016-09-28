using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CustomerService.ApiAccess.Exceptions;
using RestSharp;

namespace CustomerService.ApiAccess.ApiClient
{
    public class RestApiClient : IRestApiClient
    {
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
        /// <returns></returns>
        public TResponse SendRequest<TResponse>(string endpoint, object model, Method method) where TResponse : class, new()
        {
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentNullException("endpoint");
            }

            IRestRequest request = this.InitRequest(endpoint, model, method);
            var result = _restClient.Execute<TResponse>(request);

            this.VerifyResponse(result);

            return result.Data;
        }

        /// <summary>
        /// Send request to specified endpoint async.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="model"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<TResponse> SendRequestAsync<TResponse>(string endpoint, object model, Method method) where TResponse : class, new()
        {
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentNullException("endpoint");
            }

            IRestRequest request = this.InitRequest(endpoint, model, method);
            var result = await _restClient.ExecuteTaskAsync<TResponse>(request);

            this.VerifyResponse(result);

            return result.Data;
        }

        /// <summary>
        /// Verifies if request completed correct.
        /// Raises exception if external service returns error.
        /// </summary>
        private void VerifyResponse<TResponse>(IRestResponse<TResponse> response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new AuthorizationException();
            }
        }

        /// <summary>
        /// Initializes request data.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="model"></param>
        /// <param name="method"></param>
        /// <returns>Instance of IRestRequest</returns>
        private IRestRequest InitRequest(string endpoint, object model, Method method)
        {
            var request = new RestRequest(endpoint, method);

            if (model == null)
            {
                return request;
            }

            var modelReader = new RequestModelReader(model);

            foreach (var parameterModel in modelReader.ReadParameters())
            {
                request.AddParameter(parameterModel.Name, parameterModel.Value, parameterModel.Type);
            }

            return request;
        }
    }
}
