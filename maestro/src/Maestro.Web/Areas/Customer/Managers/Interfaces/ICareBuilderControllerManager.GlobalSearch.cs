using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos.HealthLibraryService;
using Maestro.Web.Areas.Customer.Models.CareBuilder;

namespace Maestro.Web.Areas.Customer.Managers.Interfaces
{
    /// <summary>
    /// ICareBuilderControllerManager.GlobalSearch
    /// </summary>
    public partial interface ICareBuilderControllerManager
    {
        /// <summary>
        /// Searches the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<IList<SearchEntryResponseViewModel>> GlobalSearch(GlobalSearchDto filter);
    }
}