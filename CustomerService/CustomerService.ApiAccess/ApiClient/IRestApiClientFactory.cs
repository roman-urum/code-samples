using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.ApiAccess.ApiClient
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
