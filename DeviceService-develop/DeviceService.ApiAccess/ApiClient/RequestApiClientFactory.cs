namespace DeviceService.ApiAccess.ApiClient
{
    using DeviceService.Common;

    public class RestApiClientFactory : IRestApiClientFactory
    {
        /// <summary>
        /// Creates new instance of IRestApiClient.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public IRestApiClient Create(string url)
        {
            return new RestApiClient(url);
        }
    }
}
