using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Common.Exceptions;
using Maestro.Domain.Constants;
using Maestro.Web.Extensions;
using Maestro.Web.Filters;
using Maestro.Web.Managers.Interfaces;
using Maestro.Web.Models.Users;
using Maestro.Web.Resources;
using Maestro.Web.Security;
using NLog;

namespace Maestro.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAuthenticator authenticator;
        private readonly IUsersControllerManager usersManager;
        private readonly IAuthDataStorage authDataStorage;

        public AccountController(IAuthenticator authenticator,
            IUsersControllerManager usersManager,
            IAuthDataStorage authDataStorage)
        {
            this.authenticator = authenticator;
            this.usersManager = usersManager;
            this.authDataStorage = authDataStorage;
        }

        /// <summary>
        /// Returns view with form to authenticate in Maestro
        /// </summary>
        /// <returns></returns>
        [AnonymousOnly]
        public ActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel(returnUrl));
        }

        /// <summary>
        /// Validates and authenticates user if model data is valid.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid || !(await usersManager.AuthenticateUser(model, ModelState)))
                {
                    return View(model);
                }
            }
            catch (AuthorizationException)
            {
                ModelState.AddModelError(LoginViewModel.IncorrectCredentialsKey,
                            GlobalStrings.Users_Login_IncorrectCredentialsMessage);

                return View(model);
            }

            if (!string.IsNullOrEmpty(model.ReturnUrl))
            {
                Uri rurl;

                if (Uri.TryCreate(model.ReturnUrl, UriKind.Relative, out rurl))
                {
                    return Redirect(rurl.OriginalString);
                }
            }

            if (User.IsInRole(Roles.SuperAdmin))
            {
                var currentCustomer = CustomerContext.Current.Customer;

                if (currentCustomer == null)
                {
                    return RedirectToAction("Index", "Customers");
                }

                var activeSites = currentCustomer.Sites.Where(s => s.IsActive).OrderBy(s => s.Name).ToList();

                if (!activeSites.Any())
                {
                    return RedirectToAction("NoAccess", "Sites");
                }

                return RedirectToAction("Index", "Dashboard", new
                {
                    area = "Site",
                    siteId = activeSites.First().Id
                });
            }

            return new RedirectResult(Url.SiteDefaultUrl(User));
        }

        /// <summary>
        /// Returns view to change current password.
        /// </summary>
        /// <returns></returns>
        [MaestroAuthorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Changes user password in token service.
        /// </summary>
        /// <returns></returns>
        [MaestroAuthorize]
        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid || !await this.usersManager.ChangePassword(model, ModelState))
            {
                return View(model);
            }

            return RedirectToAction("Logout");
        }

        /// <summary>
        /// Clears user cookies.
        /// </summary>
        /// <returns>Redirects to login page.</returns>
        [MaestroAuthorize]
        public ActionResult Logout()
        {
            authenticator.ClearAuthenticationSession();

            return RedirectToAction("Login");
        }

        /// <summary>
        /// Activates the account by email link.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ActivateAccount(ActivationLinkViewModel model)
        {
            var activationModel = usersManager.CreateActivateAccountModel(model);

            return View(activationModel);
        }

        /// <summary>
        /// Activates the account.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ActivateAccount(ActivateAccountViewModel model)
        {
            await usersManager.ActivateAdminWithCredentials(model);

            return RedirectToAction("Login");
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ResetPassword(ResetPasswordLinkViewModel model)
        {
            var resetPasswordModel = usersManager.CreateResetPasswordModel(model);

            return View(resetPasswordModel);
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPasswordAccountViewModel model)
        {
            if (!await usersManager.UpdatePassword(model, ModelState))
            {
                return View(model);
            }

            return RedirectToAction("Login");
        }

        /// <summary>
        /// Empty method for requests to resume session time.
        /// Authentication cookies lifetime resumes automatically
        /// when request received on server part.
        /// </summary>
        /// <returns></returns>
        public JsonResult ResumeSession()
        {
            return Json(new { success = User.Identity.IsAuthenticated });
        }

        /// <summary>
        /// Stops user session if user authenticated. 
        /// Requires to guarantie that user will be logout
        /// due to inactivity.
        /// </summary>
        /// <returns></returns>
        public JsonResult StopSession()
        {
            if (User.Identity.IsAuthenticated)
            {
                authenticator.ClearAuthenticationSession();
            }

            return Json(new { success = true });
        }

        /// <summary>
        /// Verifies if user email already exists.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<JsonResult> DoesEmailExist(BaseUserViewModel user)
        {
            // TODO: Methods should be moved to common manager from admins manager.
            var error = user.Id == default(Guid) ? await this.usersManager.ValidateCreateUser(user) :
                                       await this.usersManager.ValidateEditUser(user);

            return Json(error == null, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Resends the invite.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        [HttpPost]
        [MaestroAuthorize(Roles.SuperAdmin)]
        public async Task<JsonResult> ResendInvite(string email)
        {
            await usersManager.ResendInvite(email, null);

            return Json(new { success = true, message = GlobalStrings.Users_Resend_Success });
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [MaestroAuthorize(Roles.SuperAdmin)]
        public async Task<JsonResult> SendResetPasswordEmail(Guid id)
        {
            await usersManager.ResetPassword(id, null);

            return Json(new { success = true, message = GlobalStrings.User_ResetPassword_Success });
        }

        /// <summary>
        /// Returns modal with alert message or empty view
        /// if alert should not appear.
        /// </summary>
        /// <returns></returns>
        public ActionResult PasswordExpirationModal()
        {
            var authData = this.authDataStorage.GetUserAuthData();

            if (authData == null || !authData.PasswordExpirationUtc.HasValue || authData.IsChangePasswordAlertShown)
            {
                return new EmptyResult();
            }

            var model = authData.PasswordExpirationUtc.Value - DateTime.UtcNow;

            if (model.TotalDays > 7)
            {
                return new EmptyResult();
            }

            authData.IsChangePasswordAlertShown = true;
            authDataStorage.Save(authData);

            return PartialView("_PasswordExpirationModal", model);
        }
    }
}