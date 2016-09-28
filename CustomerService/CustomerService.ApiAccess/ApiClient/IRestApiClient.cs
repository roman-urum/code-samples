using System.Threading.Tasks;
using RestSharp;

namespace CustomerService.ApiAccess.ApiClient
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
        /// <returns></returns>
        TResponse SendRequest<TResponse>(string endpoint, object model, Method method)
            where TResponse : class, new();

        /// <summary>
        /// Send request to specified endpoint async.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="model"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        Task<TResponse> SendRequestAsync<TResponse>(string endpoint, object model, Method method)
            where TResponse : class, new();
    }
}
