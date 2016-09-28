using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos.HealthLibraryService;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// HealthLibraryService.GlobalSearch
    /// </summary>
    public partial class HealthLibraryService
    {
        /// <summary>
        /// Globals the search.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<IList<SearchEntryDto>> GlobalSearch(string token, int customerId, GlobalSearchDto filter)
        {
            var result = await healthLibraryDataProvider.GlobalSearch(token, customerId, filter);

            return result;
        }
    }
}