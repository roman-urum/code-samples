using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos.TokenService.RequestsResponses;

namespace Maestro.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IUsersService.
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Authenticates user through Token Service.
        /// </summary>
        /// <param name="credentials">User credentials.</param>
        /// <param name="clientIpAddress">The client ip address.</param>
        /// <param name="serverIpAddress">The server ip address.</param>
        /// <returns>
        /// True if user authenticated.
        /// </returns>
        Task<TokenResponseModel> AuthenticateUser(
            GetTokenRequest credentials,
            string clientIpAddress,
            string serverIpAddress
        );

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<User> GetUser(Guid id);

        /// <summary>
        /// Gets the admin users.
        /// </summary>
        /// <returns></returns>
        Task<IList<User>> GetAdminUsers();

        /// <summary>
        /// Creates the user with specified role.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="role">Role of new user.</param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<User> CreateUser(User user, string role, int customerId);

        /// <summary>
        /// Gets the user by email.
        /// Includes user role.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<User> GetUserByEmail(string email);

        /// <summary>
        /// Gets the user by email annd Id
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetUsersByEmail(string email);

        /// <summary>
        /// Edits user
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<User> EditUser(User admin, int customerId);

        /// <summary>
        /// Returns user role by role id.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<UserRole> GetUserRole(Guid roleId);

        /// <summary>
        /// Returns user role entity by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<UserRole> GetUserRole(string name);

        /// <summary>
        /// Activates the not verified user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="passwordExpiration">Password expiration in days.</param>
        /// <returns></returns>
        Task<bool> ActivateNotVerifiedUser(string email, string password, int? passwordExpiration);

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="passwordExpiration">Password expiration in days.</param>
        /// <param name="currentPassword"></param>
        /// <returns></returns>
        Task<bool> UpdatePassword(string email, string password, int? passwordExpiration,
            string currentPassword = null);

        /// <summary>
        /// Adds the user to super admin group.
        /// </summary>
        /// <param name="createdUser">The created user.</param>
        /// <returns></returns>
        Task AddUserToSuperAdminGroup(User createdUser);

        /// <summary>
        /// Sets the user groups by permissions.
        /// </summary>
        /// <param name="permissions">The permissions.</param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task SetUserGroupsByPermissions(ICollection<CustomerUserRoleToPermissionMapping> permissions, User user);

        /// <summary>
        /// Returns list of users with specified ids.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IList<User>> GetUsersByIds(IList<Guid> ids);
    }
}