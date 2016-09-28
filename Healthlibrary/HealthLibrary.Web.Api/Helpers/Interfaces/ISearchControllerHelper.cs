using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Web.Api.Models;

namespace HealthLibrary.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// ISearchControllerHelper.
    /// </summary>
    public interface ISearchControllerHelper
    {
        /// <summary>
        /// Searches the specified search request.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        Task<PagedResultDto<SearchEntryDto>> Search(int customerId, GlobalSearchDto searchRequest);
    }
}