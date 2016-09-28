using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CareInnovations.HealthHarmony.Maestro.TokenService.Models;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Interfaces
{
    /// <summary>
    /// IGroupsControllerHelper.
    /// </summary>
    public interface IGroupsControllerHelper
    {
        /// <summary>
        /// Returns list of groups for specified customer and range.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IList<GroupResponseModel>> GetGroups(int? customerId, int skip, int take);

        /// <summary>
        /// Returns required group by is.
        /// Returns null if group with specified id not exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GroupResponseModel> GetGroupById(Guid id);

        /// <summary>
        /// Creates new group in db.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        Task<GroupResponseModel> CreateGroup(CreateGroupModel group);

        /// <summary>
        /// Updates existing group record.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        Task<GroupResponseModel> UpdateGroup(Guid id, GroupModel group);

        /// <summary>
        /// Deletes group with specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>False in case if group not exists.</returns>
        Task<bool> DeleteGroup(Guid id);
    }
}
