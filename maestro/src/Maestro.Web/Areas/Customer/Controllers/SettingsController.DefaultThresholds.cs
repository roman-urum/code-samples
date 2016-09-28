using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Domain.Constants;
using Maestro.Domain.Dtos.VitalsService.Thresholds;
using Maestro.Domain.Enums;
using Maestro.Web.Helpers;

namespace Maestro.Web.Areas.Customer.Controllers
{
    /// <summary>
    /// SettingsController (customer's default thresholds management).
    /// </summary>
    public partial class SettingsController
    {
        /// <summary>
        /// Manages the thresholds.
        /// </summary>
        /// <returns></returns>
        [CustomerAuthorize(CustomerUserRolePermissions.ManageCustomerThresholds)]
        public ActionResult Thresholds()
        {
            if (User.IsInRole(Roles.SuperAdmin))
            {
                ViewBag.BreadCrumb = string.Format(
                    BreadCrumbHelper.CustomerManageThresholdsBreadcrumbTmplForSuperAdmin,
                    "\\",
                    CustomerContext.Current.Customer.Name,
                    this.Url.Action("Thresholds", "Settings")
                );
            }
            else if (User.IsInRole(Roles.CustomerUser))
            {
                ViewBag.BreadCrumb = string.Format(
                    BreadCrumbHelper.CustomerManageThresholdsBreadcrumbTmpl,
                    CustomerContext.Current.Customer.Name,
                    this.Url.Action("Thresholds", "Settings")
                );
            }

            return View("Templates");
        }

        /// <summary>
        /// Gets customer's default thresholds.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize(
            CustomerUserRolePermissions.ManagePatientThresholds,
            CustomerUserRolePermissions.ManageCustomerThresholds,
            CustomerUserRolePermissions.ManageCustomerSettings)]
        [ActionName("DefaultThresholds")]
        public async Task<ActionResult> GetDefaultThresholds(DefaultThresholdsSearchDto request)
        {
            var customerDefaultThresholds = await settingsControllerManager.GetDefaultThresholds(request);

            return Json(customerDefaultThresholds, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Creates customer's default threshold.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [CustomerAuthorize(
            CustomerUserRolePermissions.ManageCustomerThresholds,
            CustomerUserRolePermissions.ManageCustomerSettings)]
        [ActionName("DefaultThreshold")]
        public async Task<ActionResult> CreateDefaultThreshold(CreateDefaultThresholdRequestDto request)
        {
            var result = await settingsControllerManager.CreateDefaultThreshold(request);

            return Json(result);
        }

        /// <summary>
        /// Updates customer's default threshold.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        [CustomerAuthorize(
            CustomerUserRolePermissions.ManageCustomerThresholds,
            CustomerUserRolePermissions.ManageCustomerSettings)]
        [ActionName("DefaultThreshold")]
        public async Task<ActionResult> UpdateDefaultThreshold(UpdateDefaultThresholdRequestDto request)
        {
            await settingsControllerManager.UpdateDefaultThreshold(request);

            return Json(string.Empty);
        }

        /// <summary>
        /// Deletes customer's default threshold.
        /// </summary>
        /// <param name="defaultThresholdId">The default threshold identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [CustomerAuthorize(
            CustomerUserRolePermissions.ManageCustomerThresholds,
            CustomerUserRolePermissions.ManageCustomerSettings)]
        [ActionName("DefaultThreshold")]
        public async Task<ActionResult> DeleteDefaultThreshold(Guid defaultThresholdId)
        {
            await settingsControllerManager.DeleteDefaultThreshold(defaultThresholdId);

            return Json(string.Empty);
        }
    }
}