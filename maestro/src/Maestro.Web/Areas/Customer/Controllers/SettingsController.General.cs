using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;
using AutoMapper;
using Maestro.Web.Areas.Customer.Managers.Interfaces;
using Maestro.Web.Filters;
using Maestro.Domain.Dtos.CustomerService;
using Maestro.Domain.Enums;
using Maestro.Web.Areas.Customer.Models.Settings.General;
using Maestro.Web.Controllers;
using Maestro.Web.Managers.Interfaces;

namespace Maestro.Web.Areas.Customer.Controllers
{
    /// <summary>
    /// SettingsController (General customer's settings).
    /// </summary>
    [MaestroAuthorize]
    [SessionState(SessionStateBehavior.ReadOnly)]
    public partial class SettingsController : BaseController
    {
        private readonly ISettingsControllerManager settingsControllerManager;
        private readonly ICustomerUsersManager customerUsersManager;
        private readonly IUsersControllerManager usersControllerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsController"/> class.
        /// </summary>
        /// <param name="settingsControllerManager">The settings controller manager.</param>
        /// <param name="usersControllerManager">The users controller manager.</param>
        /// <param name="customerUsersManager">The customer users manager.</param>
        public SettingsController(
            ISettingsControllerManager settingsControllerManager,
            IUsersControllerManager usersControllerManager,
            ICustomerUsersManager customerUsersManager
        )
        {
            this.settingsControllerManager = settingsControllerManager;
            this.usersControllerManager = usersControllerManager;
            this.customerUsersManager = customerUsersManager;
        }

        /// <summary>
        /// Returns form with general customer settings.
        /// </summary>
        /// <returns></returns>
        [CustomerAuthorize(
            CustomerUserRolePermissions.ViewCustomerSettings,
            CustomerUserRolePermissions.ManageCustomerSettings,
            CustomerUserRolePermissions.ManageCustomerSites
        )]
        public ActionResult General()
        {
            return View("Templates");
        }

        /// <summary>
        /// Returns model with customer settings as json.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CustomerSettings(bool isBrief = true)
        {
            var customer = CustomerContext.Current.Customer;
            var model = isBrief
                ? Mapper.Map<CustomerResponseDto, GeneralSettingsViewModel>(customer)
                : Mapper.Map<CustomerResponseDto, FullCustomerViewModel>(customer);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Updates customer settings.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> CustomerSettings(GeneralSettingsViewModel model)
        {
            await this.settingsControllerManager.SaveSettings(model);

            return Json(string.Empty);
        }
    }
}