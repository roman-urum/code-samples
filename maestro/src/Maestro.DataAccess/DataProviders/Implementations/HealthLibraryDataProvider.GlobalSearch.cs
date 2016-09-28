using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.HealthLibraryService;
using RestSharp;

namespace Maestro.DataAccess.Api.DataProviders.Implementations
{
    /// <summary>
    /// HealthLibraryDataProvider.GlobalSearch
    /// </summary>
    public partial class HealthLibraryDataProvider
    {
        /// <summary>
        /// Globals the search.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<IList<SearchEntryDto>> GlobalSearch(
            string token, 
            int customerId,
            GlobalSearchDto filter
        )
        {
            var tagsQueryString = filter == null || filter.Tags == null
                ? string.Empty
                : filter.Tags.Select(t => string.Format("tags={0}", t)).Aggregate((t1, t2) => string.Format("{0}&{1}", t1,t2));

            var categoriesQueryString = filter == null || filter.Categories == null
                ? string.Empty
                : filter.Categories.Select(c => string.Format("categories={0}", c)).Aggregate((c1, c2) => string.Format("{0}&{1}", c1, c2));

            if (!string.IsNullOrEmpty(tagsQueryString) && !string.IsNullOrEmpty(categoriesQueryString))
            {
                categoriesQueryString = "&" + categoriesQueryString;
            }

            var url = string.Format("/api/{0}/search?{1}{2}", customerId, tagsQueryString, categoriesQueryString);
            var pagedResult = await apiClient.SendRequestAsync<PagedResult<SearchEntryDto>>(url, filter, Method.GET, null, token);

            return pagedResult.Results;
        }
    }
}