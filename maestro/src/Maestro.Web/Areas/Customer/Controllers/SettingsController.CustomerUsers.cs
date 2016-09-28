using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Common.Extensions;
using Maestro.Domain.Enums;
using Maestro.Web.Areas.Customer.Models.Settings.CustomerUsers;
using Maestro.Web.Extensions;
using Maestro.Web.Resources;

namespace Maestro.Web.Areas.Customer.Controllers
{
    /// <summary>
    /// SettingsController (Customer users management).
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
        public ActionResult Users()
        {
            return View("Templates");
        }

        /// <summary>
        /// Returns list of customer's users as json.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CustomerUsers()
        {
            var users = await customerUsersManager.GetCustomerUsers(CustomerContext.Current.Customer.Id);

            return Json(users, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Create new customer user using model data.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CustomerUsers(CustomerUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GenerateErrorMessageDetails());
            }

            var result = await customerUsersManager.CreateCustomerUser(model);

            if (result.IsValid)
            {
                return Json(result);
            }

            return BadRequest(result.Error.Value.GetConcatString());
        }

        /// <summary>
        /// Handles requests to update customer users.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> CustomerUsers(Guid id, CustomerUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GenerateErrorMessageDetails());
            }

            var result = await customerUsersManager.UpdateCustomerUser(id, model);

            if (result.IsValid)
            {
                return Json(string.Empty);
            }

            return BadRequest(result.Error.Value.GetConcatString());
        }

        /// <summary>
        /// Returns roles of users for current customer.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> UserRoles()
        {
            var result = await this.customerUsersManager.GetCustomerUserRoles();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Resends the invite.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        [HttpPost]
        [CustomerAuthorize(CustomerUserRolePermissions.ManageCustomerUserPassword)]
        public async Task<JsonResult> ResendInvite(string email)
        {
            await usersControllerManager.ResendInvite(email, CustomerContext.Current.Customer.PasswordExpirationDays);

            return Json(new { success = true, message = GlobalStrings.Users_Resend_Success });
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize(CustomerUserRolePermissions.ManageCustomerUserPassword)]
        public async Task<JsonResult> SendResetPasswordEmail(Guid id)
        {
            await usersControllerManager.ResetPassword(id, CustomerContext.Current.Customer.PasswordExpirationDays);

            return Json(new { success = true, message = GlobalStrings.User_ResetPassword_Success }, JsonRequestBehavior.AllowGet);
        }
    }
}