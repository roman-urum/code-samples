using System;
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
        /// Returns json with measurement.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Assessment")]
        public async Task<ActionResult> GetAssessment(Guid id)
        {
            var result = await this.careBuilderManager.GetAssessmentElement(CustomerContext.Current.Customer.Id, id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}