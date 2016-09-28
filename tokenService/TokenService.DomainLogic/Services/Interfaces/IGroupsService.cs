using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// Provides business logic for groups.
    /// </summary>
    public interface IGroupsService
    {
        /// <summary>
        /// Returns list of groups for specified customer and range.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IList<Group>> GetGroups(int? customerId, int skip, int take);

        /// <summary>
        /// Returns required group by is.
        /// Returns null if group with specified id not exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Group> GetGroupById(Guid id);

        /// <summary>
        /// Returns list of groups by provided ids.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IList<Group>> GetGroupsByIds(IList<Guid> ids);

        /// <summary>
        /// Creates new group in db.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        Task<Group> CreateGroup(Group group);

        /// <summary>
        /// Updates existing group record.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        Task<Group> UpdateGroup(Group group);

        /// <summary>
        /// Deletes the group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        Task DeleteGroup(Group group);
    }
}