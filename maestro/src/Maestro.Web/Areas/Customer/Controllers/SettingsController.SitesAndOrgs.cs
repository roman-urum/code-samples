using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Maestro.Common.Helpers;
using Maestro.Domain.Dtos.CustomerService;
using Maestro.Domain.Enums;
using Maestro.Web.Areas.Customer.Models.Settings.Sites;

namespace Maestro.Web.Areas.Customer.Controllers
{
    /// <summary>
    /// SettingsController (Sites and Orgs).
    /// </summary>
    public partial class SettingsController
    {
        /// <summary>
        /// Returns list of customer users.
        /// </summary>
        /// <returns></returns>
        [CustomerAuthorize(
            CustomerUserRolePermissions.CreateCustomerUsers,
            CustomerUserRolePermissions.ViewCustomerUsers,
            CustomerUserRolePermissions.ManageCustomerUserDetails,
            CustomerUserRolePermissions.ManageCustomerUserPassword,
            CustomerUserRolePermissions.ManageCustomerUserPermissions
        )]
        public ActionResult Sites()
        {
            return View("Templates");
        }

        /// <summary>
        /// Returns list of customer sites as json.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CustomerSites()
        {
            return Json(
                Mapper.Map<IList<SiteResponseDto>, IList<SiteViewModel>>(CustomerContext.Current.Customer.Sites)
                    .OrderBy(o => o.Name, new NaturalSortComparer())
                    .ToList(),
                JsonRequestBehavior.AllowGet
            );
        }

        /// <summary>
        /// Creates new site for current customer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Json with site id.</returns>
        [HttpPost]
        public async Task<ActionResult> CustomerSites(CreateUpdateSiteViewModel model)
        {
            var result = await settingsControllerManager.CreateCustomerSite(model);

            return Json(result);
        }

        /// <summary>
        /// Updates site for current customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Empty response.
        /// </returns>
        [HttpPut]
        public async Task<ActionResult> CustomerSites(Guid id, CreateUpdateSiteViewModel model)
        {
            await settingsControllerManager.UpdateCustomerSite(id, model);

            return Json(string.Empty);
        }

        /// <summary>
        /// Deletes site for current customer.
        /// </summary>
        /// <param name="id">The site identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> CustomerSites(Guid id)
        {
            await settingsControllerManager.DeleteCustomerSite(id);

            return Json(string.Empty);
        }

        /// <summary>
        /// Returns list of customer organizations as json.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CustomerOrganizations()
        {
            return Json(
                Mapper.Map<IList<OrganizationResponseDto>, IList<OrganizationViewModel>>(CustomerContext.Current.Customer.Organizations)
                    .OrderBy(o => o.Name, new NaturalSortComparer())
                    .ToList(),
                JsonRequestBehavior.AllowGet
            );
        }

        /// <summary>
        /// Creates new organization for current customer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Json with organization id.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> CustomerOrganizations(CreateUpdateOrganizationViewModel model)
        {
            var result = await settingsControllerManager.CreateCustomerOrganization(model);

            return Json(result);
        }

        /// <summary>
        /// Updates organization for current customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> CustomerOrganizations(Guid id, CreateUpdateOrganizationViewModel model)
        {
            await settingsControllerManager.UpdateCustomerOrganization(id, model);

            return Json(string.Empty);
        }

        /// <summary>
        /// Deletes organization for current customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> CustomerOrganizations(Guid id)
        {
            await settingsControllerManager.DeleteCustomerOrganization(id);

            return Json(string.Empty);
        }
    }
}