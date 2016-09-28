using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Maestro.Common.Extensions;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.DataAccess.EF.DataAccess;
using Maestro.Domain.Constants;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos.TokenService;
using Maestro.Domain.Dtos.TokenService.RequestsResponses;
using Maestro.DomainLogic.Exceptions;
using Maestro.DomainLogic.Services.Interfaces;
using AutoMapper;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// UsersService.
    /// </summary>
    public class UsersService : IUsersService
    {
        private const string PasswordCredentialType = "Password";

        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<User> usersRepository;
        private readonly IRepository<CustomerUser> customerUsersRepository;
        private readonly IRepository<UserRole> userRolesRepository;
        private readonly IUsersDataProvider tokenDataProvider;
        private readonly ICustomersDataProvider customersDataProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="tokenDataProvider">The token data provider.</param>
        public UsersService(IUnitOfWork unitOfWork, IUsersDataProvider tokenDataProvider, ICustomersDataProvider customersDataProvider)
        {
            this.unitOfWork = unitOfWork;
            usersRepository = this.unitOfWork.CreateGenericRepository<User>();
            customerUsersRepository = this.unitOfWork.CreateGenericRepository<CustomerUser>();
            userRolesRepository = this.unitOfWork.CreateGenericRepository<UserRole>();
            this.tokenDataProvider = tokenDataProvider;
            this.customersDataProvider = customersDataProvider;
        }

        /// <summary>
        /// Authenticates user through Token Service.
        /// </summary>
        /// <param name="credentials">User credentials.</param>
        /// <param name="clientIpAddress">The client ip address.</param>
        /// <param name="serverIpAddress">The server ip address.</param>
        /// <returns>
        /// True if user authenticated.
        /// </returns>
        public async Task<TokenResponseModel> AuthenticateUser(
            GetTokenRequest credentials,
            string clientIpAddress,
            string serverIpAddress
        )
        {
            return await this.tokenDataProvider.AuthenticateUser(
                credentials, 
                clientIpAddress,
                serverIpAddress
            );
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<User> GetUser(Guid id)
        {
            var user = await usersRepository.FindAsync(u => u.Id == id);

            return user.SingleOrDefault();
        }

        /// <summary>
        /// Gets the admin users.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<User>> GetAdminUsers()
        {
            var includes = new List<Expression<Func<User, object>>>
            {
                e => e.Role,
            };

            var adminUsers = await usersRepository.FindAsync(u =>
                u.Role.Name.Equals(Roles.SuperAdmin),
                o => o.OrderBy(u => u.FirstName),
                includes);

            return adminUsers.ToList();
        }

        /// <summary>
        /// Creates the user with specified role.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="role">Role which should be assigned to user.</param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<User> CreateUser(User user, string role, int customerId)
        {
            var roleEntity = await this.GetUserRole(role);

            if (roleEntity == null)
            {
                throw new DataNotFoundException("Role with name {0} not exists".FormatWith(role));
            }

            var createPrincipalModel = Mapper.Map<CreatePrincipalModel>(user);
            createPrincipalModel.CustomerId = customerId;

            var principal = await tokenDataProvider.CreateUser(createPrincipalModel);

            user.RoleId = roleEntity.Id;
            user.TokenServiceUserId = principal.Id.ToString();
            usersRepository.Insert(user);

            await unitOfWork.SaveAsync();

            return user;
        }

        /// <summary>
        /// Modifies user data in Maestro db and in token service.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<User> EditUser(User user, int customerId)
        {
            var existedUser = (await usersRepository.FindAsync(u => u.Id == user.Id)).FirstOrDefault();

            if (existedUser == null)
            {
                throw new DataNotFoundException("User with id {0} not exists".FormatWith(user.Id));
            }

            var updateRequest = Mapper.Map<UpdatePrincipalModel>(user);
            await this.tokenDataProvider.UpdatePrincipals(Guid.Parse(existedUser.TokenServiceUserId), updateRequest);

            existedUser.IsEnabled = user.IsEnabled;
            existedUser.FirstName = user.FirstName;
            existedUser.LastName = user.LastName;
            existedUser.Email = user.Email;
            existedUser.Phone = user.Phone;

            await unitOfWork.SaveAsync();

            return existedUser;
        }

        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public async Task<User> GetUserByEmail(string email)
        {
            // We are doing two roundtrips instead of one to include CustomerUser properties
            // See details: https://data.uservoice.com/forums/72025-entity-framework-feature-suggestions/suggestions/1249289-eager-loading-for-properties-of-derived-classes

            var customerUser = (await customerUsersRepository
                .FindAsync(
                    cu => cu.Email.ToLower() == email.ToLower(),
                    null,
                    new List<Expression<Func<CustomerUser, object>>>
                    {
                        e => e.Role,
                        e => e.CustomerUserRole,
                        e => e.CustomerUserRole.Permissions,
                        e => e.CustomerUserSites
                    }
                ))
                .FirstOrDefault();

            if (customerUser != null)
            {
                return customerUser;
            }

            return (await usersRepository.FindAsync(u => u.Email.ToLower() == email.ToLower())).FirstOrDefault();
        }

        /// <summary>
        /// Returns user role by role id.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<UserRole> GetUserRole(Guid roleId)
        {
            var role = await userRolesRepository.FindAsync(r => r.Id == roleId);

            return role.FirstOrDefault();
        }

        /// <summary>
        /// Returns user role entity by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<UserRole> GetUserRole(string name)
        {
            var userRole = await this.userRolesRepository.FindAsync(u => u.Name.Equals(name),
                o => o.OrderBy(u => u.Name));

            return userRole.FirstOrDefault();
        }

        /// <summary>
        /// Activates the not verified user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="passwordExpiration">Password expiration in days.</param>
        /// <returns></returns>
        public async Task<bool> ActivateNotVerifiedUser(string email, string password, int? passwordExpiration)
        {
            var user = await usersRepository.FindAsync(u => !u.IsEmailVerified && u.Email == email);

            var userToUpdate = user.FirstOrDefault();

            if (userToUpdate != null)
            {
                await UpdatePrincipalCredentials(userToUpdate, email, password, passwordExpiration);

                userToUpdate.IsEmailVerified = true;
                usersRepository.Update(userToUpdate);

                await unitOfWork.SaveAsync();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="passwordExpiration">Password expiration in days.</param>
        /// <param name="currentPassword"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePassword(string email, string password, int? passwordExpiration,
            string currentPassword = null)
        {
            var user = await usersRepository.FindAsync(u => u.Email == email);

            var userToUpdate = user.FirstOrDefault();

            if (userToUpdate != null)
            {
                await UpdatePrincipalCredentials(userToUpdate, email, password, passwordExpiration, currentPassword);

                usersRepository.Update(userToUpdate);

                await unitOfWork.SaveAsync();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the users by email
        /// </summary>
        /// <param name="email">user's email</param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetUsersByEmail(string email)
        {
            var user = await usersRepository.FindAsync(u => u.Email == email);

            return user;
        }

        /// <summary>
        /// Adds the user to super admin group.
        /// </summary>
        /// <param name="createdUser">The created user.</param>
        /// <returns></returns>
        public async Task AddUserToSuperAdminGroup(User createdUser)
        {
            var principalsResponse = await tokenDataProvider.GetPrincipals(createdUser.TokenServiceUserId);

            principalsResponse.Groups.Add(TokenServiceGroupGuids.SuperAdmin);

            var principalsUpdateRequest = Mapper.Map<UpdatePrincipalModel>(principalsResponse);

            await tokenDataProvider.UpdatePrincipals(principalsResponse.Id, principalsUpdateRequest);
        }

        /// <summary>
        /// Sets the user groups by permissions.
        /// </summary>
        /// <param name="permissions">The permissions.</param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task SetUserGroupsByPermissions(ICollection<CustomerUserRoleToPermissionMapping> permissions, User user)
        {
            var groups = new List<Guid>();

            foreach (var singlePermission in permissions)
            {
                groups.Add(PermissionInfo.Infos[singlePermission.PermissionCode].TokenServiceGroupId);
            }

            if (groups.Any())
            {
                var principals = await tokenDataProvider.GetPrincipals(user.TokenServiceUserId);
                var principalsUpdateRequest = Mapper.Map<PrincipalResponseModel, UpdatePrincipalModel>(principals);

                if (principalsUpdateRequest.Groups == null)
                {
                    principals.Groups = new List<Guid>();
                }

                principalsUpdateRequest.Groups = groups.Distinct().ToList();
                await tokenDataProvider.UpdatePrincipals(principals.Id, principalsUpdateRequest);
            }
        }

        /// <summary>
        /// Returns list of users with specified ids.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IList<User>> GetUsersByIds(IList<Guid> ids)
        {
            return await usersRepository.FindAsync(u => ids.Contains(u.Id));
        }

        #region Private methods

        /// <summary>
        /// Updates username and credentials of principal related with specified user.
        /// </summary>
        /// <param name="userToUpdate">The user to update.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="passwordExpiration">Password expiration in days.</param>
        /// <param name="currentPassword"></param>
        /// <returns></returns>
        private async Task UpdatePrincipalCredentials(User userToUpdate, string email, string password,
            int? passwordExpiration, string currentPassword = null)
        {
            var principals = await tokenDataProvider.GetPrincipals(
                userToUpdate.TokenServiceUserId);

            var principalsToUpdate = Mapper.Map<UpdatePrincipalModel>(principals);

            principalsToUpdate.Username = email;
            principalsToUpdate.Credential = new CredentialUpdateModel
            {
                Value = password,
                Type = PasswordCredentialType,
                CurrentValue = currentPassword
            };

            if (passwordExpiration.HasValue)
            {
                principalsToUpdate.Credential.ExpiresUtc = DateTime.UtcNow.AddDays(passwordExpiration.Value);
            }

            await tokenDataProvider.UpdatePrincipals(principals.Id, principalsToUpdate);
        }

        #endregion
    }
}