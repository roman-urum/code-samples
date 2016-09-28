using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Domain.Dtos.VitalsService.Conditions;
using Maestro.Domain.Enums;

namespace Maestro.Web.Areas.Customer.Controllers
{
    /// <summary>
    /// SettingsController (Conditions management).
    /// </summary>
    public partial class SettingsController
    {
        /// <summary>
        /// Returns list of customer conditions.
        /// </summary>
        /// <returns></returns>
        [CustomerAuthorize(
            CustomerUserRolePermissions.ManageCustomerSettings
        )]
        public ActionResult Conditions()
        {
            return View("Templates");
        }

        /// <summary>
        /// Retrieves a list of customer's conditions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CustomerConditions()
        {
            var conditions = await settingsControllerManager
                .GetCustomerConditions(CustomerContext.Current.Customer.Id);

            return Json(conditions, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Customers the conditions.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CustomerConditions(ConditionRequestDto request)
        {
            var customerId = CustomerContext.Current.Customer.Id;

            var result = await settingsControllerManager.CreateCustomerCondition(customerId, request);

            return Json(result);
        }

        /// <summary>
        /// Customers the conditions.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> CustomerConditions(Guid id, ConditionRequestDto request)
        {
            var customerId = CustomerContext.Current.Customer.Id;

            await settingsControllerManager.UpdateCustomerCondition(customerId, id, request);

            return Json(string.Empty);
        }

        /// <summary>
        /// Returns list of conditions with rates.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CustomerConditionsTags()
        {
            var result = await this.settingsControllerManager.GetConditionsTags();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}