namespace HealthLibrary.ApiAccess.ApiClient
{
    /// <summary>
    /// Factory to build api client.
    /// </summary>
    public interface IRestApiClientFactory
    {
        /// <summary>
        /// Creates new instance of IRestApiClient.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        IRestApiClient Create(string url);
    }
}
