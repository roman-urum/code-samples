using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace DeviceService.ApiAccess.Extensions
{
    /// <summary>
    /// Contains extension methods for IRestResponse instances.
    /// </summary>
    internal static class RestResponseExtensions
    {
        /// <summary>
        /// Reads response in required format.
        /// Contains fix for iHeath API responses issue.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static TResponse GetData<TResponse>(this IRestResponse<TResponse> target)
        {
            // Fix for iHealth API responses:
            // iHealth provides response content in json format but with content-type "text/html"
            if (target.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(target.Content) && target.Data == null)
            {
                return JsonConvert.DeserializeObject<TResponse>(target.Content);
            }

            return target.Data;
        }
    }
}
