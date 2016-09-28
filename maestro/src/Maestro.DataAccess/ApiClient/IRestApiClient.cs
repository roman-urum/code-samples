using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace Maestro.DataAccess.Api.ApiClient
{
    /// <summary>
    /// IRestApiClient.
    /// </summary>
    public interface IRestApiClient
    {
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
        TResponse SendRequest<TResponse>(
            string endpoint,
            object model, 
            Method method,
            Dictionary<string, string> headers = null,
            string bearerToken = null
        ) where TResponse : class, new();

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
        Task<TResponse> SendRequestAsync<TResponse>(
            string endpoint, 
            object model, 
            Method method,
            Dictionary<string, string> headers = null,
            string bearerToken = null
        ) where TResponse : class, new();

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
        Task SendRequestAsync(
            string endpoint,
            object model, 
            Method method,
            Dictionary<string, string> headers = null,
            string bearerToken = null
        );
    }
}