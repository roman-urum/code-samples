using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Maestro.Common;
using Maestro.Common.Extensions;
using Maestro.Domain.Constants;
using Maestro.Domain.DbEntities;
using Maestro.DomainLogic.Exceptions;
using Maestro.DomainLogic.Services.Interfaces;
using Maestro.Web.Areas.Settings.Models.Admins;
using Maestro.Web.Exceptions;
using Maestro.Web.Managers.Interfaces;
using Maestro.Web.Models.Users;

namespace Maestro.Web.Managers.Implementations
{
    using System.Linq;

    public class AdminsControllerManager : IAdminsControllerManager
    {
        private readonly IUsersService usersService;
        private readonly IEmailManager emailManager;

        public AdminsControllerManager(IUsersService usersService, 
            IEmailManager emailManager)
        {
            this.usersService = usersService;
            this.emailManager = emailManager;
        }

        /// <summary>
        /// Gets the admins ordered by first name.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AdminListModel>> GetAdmins()
        {
            var adminUsers = await usersService.GetAdminUsers();

            return adminUsers.Select(Mapper.Map<AdminListModel>).OrderBy(u => u.FirstName);
        }

        /// <summary>
        /// Creates the admin.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<UserViewModel> CreateAdmin(UserViewModel model)
        {
            var user = Mapper.Map<UserViewModel, User>(model);
            var createdUser = await usersService.CreateUser(user, 
                Roles.SuperAdmin, Settings.AdminDefaultCustomerId);
            
            await usersService.AddUserToSuperAdminGroup(createdUser);
            await emailManager.SendActivationEmail(createdUser, null);

            var createdUserModel = Mapper.Map<User, UserViewModel>(createdUser);
            createdUserModel.Role = "Maestro Admin";

            return createdUserModel;
        }

        public async Task<UserViewModel> EditAdmin(UserViewModel adminModel)
        {
            var admin = Mapper.Map<User>(adminModel);

            var editedUser = await usersService.EditUser(admin, Settings.AdminDefaultCustomerId);

            return Mapper.Map<UserViewModel>(editedUser);
        }

        public async Task<UserViewModel> GetAdmin(Guid id)
        {
            var admin = await usersService.GetUser(id);

            UserViewModel adminModel = Mapper.Map<UserViewModel>(admin);

            return adminModel;
        }

        /// <summary>
        /// Updates isenabled field for user with specified id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        public async Task SetEnabledState(Guid userId, bool isEnabled)
        {
            var user = await usersService.GetUser(userId);

            if (user == null)
            {
                throw new DataNotFoundException("user", userId);
            }

            if (!await IsAdmin(user.RoleId))
            {
                throw new ServiceUsageException(
                    "User with id {0} cannot be modified in current request.".FormatWith(userId));
            }

            user.IsEnabled = isEnabled;

            await usersService.EditUser(user, Settings.AdminDefaultCustomerId);
        }

        /// <summary>
        /// Verifies if role with specified id is maestro admin role.
        /// </summary>
        /// <param name="userRoleId"></param>
        /// <returns></returns>
        public async Task<bool> IsAdmin(Guid userRoleId)
        {
            var userRole = await this.usersService.GetUserRole(userRoleId);

            return userRole.Name.Equals(Roles.SuperAdmin);
        }
    }
}