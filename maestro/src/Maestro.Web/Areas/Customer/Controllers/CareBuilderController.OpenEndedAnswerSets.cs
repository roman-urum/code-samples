using System.Threading.Tasks;
using System.Web.Mvc;

namespace Maestro.Web.Areas.Customer.Controllers
{
    /// <summary>
    /// CareBuilderController.
    /// </summary>
    public partial class CareBuilderController
    {
        /// <summary>
        /// Gets the open ended answer set.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("OpenEndedAnswerSet")]
        public async Task<ActionResult> GetOpenEndedAnswerSet()
        {
            var result = await this.careBuilderManager.GetOpenEndedAnswerSet();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}