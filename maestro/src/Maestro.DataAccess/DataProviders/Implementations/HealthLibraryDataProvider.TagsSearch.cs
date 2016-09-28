using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using RestSharp;

namespace Maestro.DataAccess.Api.DataProviders.Implementations
{
    /// <summary>
    /// HealthLibraryDataProvider.TagsSearch
    /// </summary>
    public partial class HealthLibraryDataProvider
    {
        /// <summary>
        /// Searches the tags.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        public async Task<IList<string>> SearchTags(
            string token, 
            int customerId,
            string term
        )
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("/api/{0}/tags", customerId));

            if (!string.IsNullOrEmpty(term))
            {
                sb.Append(string.Format("?q={0}", term));
            }

            var url = sb.ToString();

            var pagedResult = await apiClient.SendRequestAsync<PagedResult<string>>(url, null, Method.GET, null, token);

            return pagedResult.Results;
        }
    }
}