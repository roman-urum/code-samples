using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos.HealthLibraryService;

namespace Maestro.DataAccess.Api.DataProviders.Interfaces
{
    /// <summary>
    /// IHealthLibraryDataProvider.GlobalSearch
    /// </summary>
    public partial interface IHealthLibraryDataProvider
    {
        /// <summary>
        /// Globals the search.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<IList<SearchEntryDto>> GlobalSearch(string token, int customerId, GlobalSearchDto filter);
    }
}