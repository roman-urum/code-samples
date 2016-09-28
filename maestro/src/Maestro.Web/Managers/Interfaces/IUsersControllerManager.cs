using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Domain.DbEntities;
using Maestro.Web.Models.Users;

namespace Maestro.Web.Managers.Interfaces
{
    using Maestro.Web.Security;

    public interface IUsersControllerManager
    {
        /// <summary>
        /// Activates the admin.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        ActivateAccountViewModel CreateActivateAccountModel(ActivationLinkViewModel model);

        /// <summary>
        /// Activates the admin with credentials.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task ActivateAdminWithCredentials(ActivateAccountViewModel model);

        /// <summary>
        /// Resends the invite.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="passwordExpiration"></param>
        /// <returns></returns>
        Task ResendInvite(string email, int? passwordExpiration);

        /// <summary>
        /// Validates created user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<string> ValidateCreateUser(BaseUserViewModel model);


        /// <summary>
        /// Validates edited user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> ValidateEditUser(BaseUserViewModel model);

        /// <summary>
        /// Authenticates user in system.
        /// </summary>
        /// <returns>
        /// True if user authenticated successfully.
        /// </returns>
        Task<bool> AuthenticateUser(LoginViewModel model, ModelStateDictionary modelState);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="passwordExpiration"></param>
        /// <returns></returns>
        Task ResetPassword(Guid id, int? passwordExpiration);

        /// <summary>
        /// Creates the reset password model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        ResetPasswordAccountViewModel CreateResetPasswordModel(ResetPasswordLinkViewModel model);

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="modelState"></param>
        /// <returns></returns>
        Task<bool> UpdatePassword(ResetPasswordAccountViewModel model, ModelStateDictionary modelState);

        /// <summary>
        /// Changes user's password using model data.
        /// Includes validation by current password value.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelState"></param>
        /// <returns></returns>
        Task<bool> ChangePassword(ChangePasswordViewModel model, ModelStateDictionary modelState);
    }
}
