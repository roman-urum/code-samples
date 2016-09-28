using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos.HealthLibraryService;

namespace Maestro.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IHealthLibraryService.GlobalSearch
    /// </summary>
    public partial interface IHealthLibraryService
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