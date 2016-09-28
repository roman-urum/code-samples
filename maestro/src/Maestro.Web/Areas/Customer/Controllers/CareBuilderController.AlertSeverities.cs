using System.Threading.Tasks;
using System.Web.Mvc;

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
        /// <returns></returns>
        [HttpGet]
        [ActionName("AlertSeverities")]
        public async Task<ActionResult> GetAlertSeverities()
        {
            var searchResult = await careBuilderManager.GetAlertSeverities();

            return Json(searchResult, JsonRequestBehavior.AllowGet);
        }
    }
}