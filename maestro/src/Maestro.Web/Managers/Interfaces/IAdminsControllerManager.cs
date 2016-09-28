using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Web.Areas.Settings.Models.Admins;
using Maestro.Web.Models.Users;

namespace Maestro.Web.Managers.Interfaces
{
    public interface IAdminsControllerManager
    {
        /// <summary>
        /// Gets the admins.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AdminListModel>> GetAdmins();

        /// <summary>
        /// Gets admin by id
        /// </summary>
        /// <param name="id">admin id</param>
        /// <returns>admin instance</returns>
        Task<UserViewModel> GetAdmin(Guid id);

        /// <summary>
        /// Creates the admin.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<UserViewModel> CreateAdmin(UserViewModel model);

        /// <summary>
        /// Edits admin
        /// </summary>
        /// <param name="editedAdmin"></param>
        /// <returns></returns>
        Task<UserViewModel> EditAdmin(UserViewModel editedAdmin);

        /// <summary>
        /// Updates isenabled field for user with specified id
        /// (only for customer users).
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        Task SetEnabledState(Guid userId, bool isEnabled);
    }
}