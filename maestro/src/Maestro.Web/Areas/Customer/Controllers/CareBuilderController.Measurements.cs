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
        /// Returns json with list of measurements for requested customer.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Measurements()
        {
            var result = await this.careBuilderManager.GetMeasurementElements(CustomerContext.Current.Customer.Id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Returns json with measurement.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Measurement")]
        public async Task<ActionResult> GetMeasurement(Guid id)
        {
            var result = await this.careBuilderManager.GetMeasurementElement(CustomerContext.Current.Customer.Id, id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}