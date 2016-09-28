using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Maestro.Common;
using Maestro.Common.Exceptions;
using Maestro.Domain.Constants;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos.TokenService.RequestsResponses;
using Maestro.DomainLogic.Services.Interfaces;
using Maestro.Web.Exceptions;
using Maestro.Web.Helpers;
using Maestro.Web.Managers.Interfaces;
using Maestro.Web.Models.Users;
using Maestro.Web.Resources;
using Maestro.Web.Security;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;

namespace Maestro.Web.Managers.Implementations
{
    /// <summary>
    /// UsersControllerManager.
    /// </summary>
    public class UsersControllerManager : IUsersControllerManager
    {
        private readonly IUsersService usersService;
        private readonly ICustomersService customersService;
        private readonly IEmailManager emailManager;
        private readonly IAuthenticator authenticator;
        private readonly IAuthDataStorage authDataStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersControllerManager"/> class.
        /// </summary>
        /// <param name="usersService">The users service.</param>
        /// <param name="customersService">The customers service.</param>
        /// <param name="emailManager">The email manager.</param>
        /// <param name="authenticator">The authenticator.</param>
        /// <param name="authDataStorage">The auth data storage.</param>
        public UsersControllerManager(
            IUsersService usersService,
            ICustomersService customersService,
            IEmailManager emailManager,
            IAuthenticator authenticator,
            IAuthDataStorage authDataStorage)
        {
            this.usersService = usersService;
            this.emailManager = emailManager;
            this.authenticator = authenticator;
            this.customersService = customersService;
            this.authDataStorage = authDataStorage;
        }

        /// <summary>
        /// Activates the admin.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ActivateAccountViewModel CreateActivateAccountModel(ActivationLinkViewModel model)
        {
            var activationModel = Mapper.Map<ActivationLinkViewModel,
                ActivateAccountViewModel>(model);

            return activationModel;
        }

        /// <summary>
        /// Activates the admin with credentials.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="LogicException"></exception>
        public async Task ActivateAdminWithCredentials(ActivateAccountViewModel model)
        {
            VerifyTokenAndTime(model);

            var result = await usersService.ActivateNotVerifiedUser(model.Email, model.Password, model.PasswordExpiration);

            if (!result)
            {
                throw new LogicException(GlobalStrings.ActivateAccount_UserNotFoundOrActivated);
            }
        }

        /// <summary>
        /// Verifies the token and time.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <exception cref="LogicException">
        /// </exception>
        private void VerifyTokenAndTime(ActivateAccountViewModel model)
        {
            var keys = string.Format("{0}{1}{2}", model.Email, model.Expires, model.PasswordExpiration);

            var token = HmacGenerator.GetHash(keys);

            var currentUnixTime = (Int32) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            if (currentUnixTime > model.Expires)
            {
                throw new LogicException(GlobalStrings.ActivateAccount_ActivationLinkExpired);
            }
            if (model.Token != token)
            {
                throw new LogicException(GlobalStrings.ActivateAccount_TokenIsNotValid);
            }
        }

        /// <summary>
        /// Resends the invite.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="passwordExpiration"></param>
        /// <returns></returns>
        public async Task ResendInvite(string email, int? passwordExpiration)
        {
            var user = await usersService.GetUserByEmail(email);

            await emailManager.SendActivationEmail(user, passwordExpiration);
        }

        /// <summary>
        /// Validates created user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<string> ValidateCreateUser(BaseUserViewModel model)
        {
            var user = await usersService.GetUserByEmail(model.Email.Trim());

            if (user != null)
            {
                return await Task.FromResult(GlobalStrings.CreateAdmin_EmailIsNotUnique);
            }

            return null;
        }

        /// <summary>
        /// Validates edited user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> ValidateEditUser(BaseUserViewModel model)
        {
            var users = await usersService.GetUsersByEmail(model.Email.Trim());

            if (users == null || !users.Any() || (users.Count() == 1 && users.First().Id == model.Id))
            {
                return null;
            }

            return await Task.FromResult(GlobalStrings.CreateAdmin_EmailIsNotUnique);
        }

        /// <summary>
        /// Authenticates user in system.
        /// </summary>
        /// <returns>
        /// True if user authenticated successfully.
        /// </returns>
        public async Task<bool> AuthenticateUser(LoginViewModel model, ModelStateDictionary modelState)
        {
            var credentials = Mapper.Map<GetTokenRequest>(model);

            TokenResponseModel response = await usersService.AuthenticateUser(
                credentials,
                HttpContext.Current.Request.UserHostAddress,
                HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"]
                );

            User user = await usersService.GetUserByEmail(model.Email);

            if (user != null)
            {
                if (!user.IsEnabled)
                {
                    modelState.AddModelError(LoginViewModel.IncorrectCredentialsKey,
                        GlobalStrings.Users_Login_DisabledAccountMessage);

                    return false;
                }

                PermissionsAuthData permissions = null;
                IList<Guid> siteIds = null;
                TimeSpan sessionTimeout = Settings.DefaultSessionTimeout;

                if (user is CustomerUser)
                {
                    var customerUser = (CustomerUser) user;

                    var customer = await customersService.GetCustomer(customerUser.CustomerId, response.Id);

                    if (customer == null)
                    {
                        modelState.AddModelError(
                            LoginViewModel.IncorrectCredentialsKey,
                            GlobalStrings.Users_Login_DisabledAccountMessage
                            );

                        return false;
                    }

                    if (customerUser.CustomerUserSites != null)
                    {
                        siteIds = customer.Sites
                            .Where(s => s.IsActive && customerUser.CustomerUserSites.Any(us => us.SiteId == s.Id))
                            .OrderBy(s => s.Name)
                            .Select(s => s.Id)
                            .ToList();
                    }

                    sessionTimeout = new TimeSpan(0, customer.IddleSessionTimeout, 0);
                    permissions = new PermissionsAuthData(customerUser.CustomerUserRole, customer.Subdomain);
                }

                authenticator.StartAuthenticationSession(
                    user,
                    response,
                    user.Role.Name,
                    sessionTimeout,
                    permissions,
                    siteIds
                    );

                return true;
            }

            return false;
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task ResetPassword(Guid id, int? passwordExpiration)
        {
            var user = await usersService.GetUser(id);

            if (!user.IsEmailVerified)
            {
                throw new LogicException(GlobalStrings.ResetPassword_UserHasNotActivated);
            }

            await emailManager.SendResetPasswordEmail(user, passwordExpiration);
        }

        /// <summary>
        /// Creates the reset password model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ResetPasswordAccountViewModel CreateResetPasswordModel(ResetPasswordLinkViewModel model)
        {
            var resetPasswordModel = Mapper.Map<ResetPasswordLinkViewModel, ResetPasswordAccountViewModel>(model);

            return resetPasswordModel;
        }

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePassword(ResetPasswordAccountViewModel model, ModelStateDictionary modelState)
        {
            VerifyTokenAndTime(model);

            try
            {
                var result = await usersService.UpdatePassword(model.Email, model.Password, model.PasswordExpiration);

                if (!result)
                {
                    throw new LogicException(GlobalStrings.UpdatePassword_UserNotFound);
                }
            }
            catch (ServiceException ex)
            {
                switch (ex.ErrorKey)
                {
                    case ServicesErrors.TokenService.CredentialAlreadyUsed:
                        modelState.AddModelError("Password", GlobalStrings.Users_ChangePassword_CredentialAlreadyUsed);
                        break;

                    default:
                        modelState.AddModelError(ChangePasswordViewModel.IncorrectCredentialsKey,
                            GlobalStrings.Users_Login_IncorrectCredentialsMessage);
                        break;
                }
            }

            return modelState.IsValid;
        }

        /// <summary>
        /// Changes user's password using model data.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public async Task<bool> ChangePassword(ChangePasswordViewModel model, ModelStateDictionary modelState)
        {
            var userData = authDataStorage.GetUserAuthData();
            int? passwordExpiration = null;

            if (!userData.Role.Equals(Roles.SuperAdmin))
            {
                passwordExpiration = CustomerContext.Current.Customer.PasswordExpirationDays;
            }

            try
            {
                if (!await usersService.UpdatePassword(
                    model.Email, model.NewPassword, passwordExpiration, model.CurrentPassword))
                {
                    modelState.AddModelError(ChangePasswordViewModel.IncorrectCredentialsKey,
                        GlobalStrings.Users_Login_IncorrectCredentialsMessage);
                }
            }
            catch (ServiceException ex)
            {
                switch (ex.ErrorKey)
                {
                    case ServicesErrors.TokenService.InvalidCredentialValue:
                        modelState.AddModelError(ChangePasswordViewModel.IncorrectCredentialsKey,
                            GlobalStrings.Users_Login_IncorrectCredentialsMessage);
                        break;

                    case ServicesErrors.TokenService.CredentialAlreadyUsed:
                        modelState.AddModelError("NewPassword", GlobalStrings.Users_ChangePassword_CredentialAlreadyUsed);
                        break;

                    default:
                        modelState.AddModelError(ChangePasswordViewModel.IncorrectCredentialsKey,
                            GlobalStrings.Users_Login_IncorrectCredentialsMessage);
                        break;
                }
            }

            return modelState.IsValid;
        }
    }
}