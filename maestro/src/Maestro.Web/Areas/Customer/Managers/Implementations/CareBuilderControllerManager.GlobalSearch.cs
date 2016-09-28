using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Hosting;
using AutoMapper;
using Maestro.Domain.Dtos.HealthLibraryService;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;
using Maestro.Web.Areas.Customer.Models.CareBuilder;
using WebGrease.Css.Extensions;

namespace Maestro.Web.Areas.Customer.Managers.Implementations
{
    /// <summary>
    /// CareBuilderControllerManager.GlobalSearch
    /// </summary>
    public partial class CareBuilderControllerManager
    {
        /// <summary>
        /// Searches the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<IList<SearchEntryResponseViewModel>> GlobalSearch(GlobalSearchDto filter)
        {
            var token = authDataStorage.GetToken();

            var resuts = await healthLibraryService.GlobalSearch(token, CustomerContext.Current.Customer.Id, filter);
           
            return Mapper.Map<IList<SearchEntryDto>, IList<SearchEntryResponseViewModel>>(resuts);
        }
    }
}