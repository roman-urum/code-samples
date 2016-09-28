using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;
using CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Interfaces;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// Provides business logic for groups.
    /// </summary>
    public class GroupsService : IGroupsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Group> groupsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupsService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public GroupsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.groupsRepository = unitOfWork.CreateGenericRepository<Group>();
        }

        /// <summary>
        /// Returns list of groups for specified customer and range.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<IList<Group>> GetGroups(int? customerId, int skip, int take)
        {
            var result =
                await this.groupsRepository.FindPagedAsync(x => !customerId.HasValue || x.Customer == customerId.Value,
                    groups => groups.OrderBy(g => g.Id), null, skip, take);

            return result.Results;
        }

        /// <summary>
        /// Returns required group by is.
        /// Returns null if group with specified id not exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Group> GetGroupById(Guid id)
        {
            var result = await this.groupsRepository.FindAsync(g => g.Id == id && !g.IsDeleted);

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Returns list of groups by provided ids.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IList<Group>> GetGroupsByIds(IList<Guid> ids)
        {
            return await this.groupsRepository.FindAsync(g => ids.Any(id => id == g.Id) && !g.IsDeleted);
        }

        /// <summary>
        /// Creates new group in db.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public async Task<Group> CreateGroup(Group group)
        {
            this.groupsRepository.Insert(group);

            await this.unitOfWork.SaveAsync();

            return group;
        }

        /// <summary>
        /// Updates existing group record.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public async Task<Group> UpdateGroup(Group group)
        {
            var entity = await this.GetGroupById(group.Id);

            if (entity == null)
            {
                return null;
            }

            entity.Policies.Clear();

            entity.Description = group.Description;
            entity.Name = group.Name;
            entity.Disabled = group.Disabled;
            entity.Customer = group.Customer;
            entity.Policies = group.Policies;

            await unitOfWork.SaveAsync();

            return entity;
        }

        /// <summary>
        /// Deletes the group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        public async Task DeleteGroup(Group group)
        {
            groupsRepository.Delete(group);

            await unitOfWork.SaveAsync();
        }
    }
}