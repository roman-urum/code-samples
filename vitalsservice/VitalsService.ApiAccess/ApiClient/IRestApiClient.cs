using System.Threading.Tasks;
using RestSharp;

namespace DeviceService.ApiAccess.ApiClient
{
    public interface IRestApiClient
    {
        /// <summary>
        /// Send request to specified endpoint.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="model"></param>
        /// <param name="method"></param>
        /// <param name="isAuthorized">Identifies if request should includes authorization token.</param>
        /// <returns></returns>
        TResponse SendRequest<TResponse>(string endpoint, object model, Method method, string authToken)
            where TResponse : class, new();

        /// <summary>
        /// Send request to specified endpoint async.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="model"></param>
        /// <param name="method"></param>
        /// <param name="isAuthorized">Identifies if request should includes authorization token.</param>
        /// <returns></returns>
        Task<TResponse> SendRequestAsync<TResponse>(string endpoint, object model, Method method, string authToken)
            where TResponse : class, new();

        /// <summary>
        /// Send request to specified endpoint async.
        /// (Without reading of response data).
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="model"></param>
        /// <param name="method"></param>
        /// <param name="isAuthorized">Identifies if request should includes authorization token.</param>
        /// <returns></returns>
        Task SendRequestAsync(string endpoint, object model, Method method, string authToken);
    }
}
