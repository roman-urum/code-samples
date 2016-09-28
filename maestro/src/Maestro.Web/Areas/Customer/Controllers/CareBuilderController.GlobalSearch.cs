using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Domain.Dtos.HealthLibraryService;

namespace Maestro.Web.Areas.Customer.Controllers
{
    /// <summary>
    /// CareBuilderController.GlobalSearch
    /// </summary>
    public partial class CareBuilderController
    {
        /// <summary>
        /// Performs global search.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Search")]
        public async Task<ActionResult> GlobalSearch(GlobalSearchDto filter)
        {
            var searchResult = await careBuilderManager.GlobalSearch(filter);

            return Json(searchResult, JsonRequestBehavior.AllowGet);
        }
    }
}