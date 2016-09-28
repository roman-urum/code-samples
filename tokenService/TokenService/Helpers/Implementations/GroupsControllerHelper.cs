using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;
using CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Interfaces;
using CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Interfaces;
using CareInnovations.HealthHarmony.Maestro.TokenService.Models;
using StackExchange.Redis;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Implementations
{
    /// <summary>
    /// GroupsControllerHelper.
    /// </summary>
    public class GroupsControllerHelper : IGroupsControllerHelper
    {
        private readonly IGroupsService groupsService;
        private readonly IDatabase redisCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupsControllerHelper" /> class.
        /// </summary>
        /// <param name="groupsService">The groups service.</param>
        /// <param name="redisCache">The redis cache.</param>
        public GroupsControllerHelper(
            IGroupsService groupsService,
            IDatabase redisCache
        )
        {
            this.groupsService = groupsService;
            this.redisCache = redisCache;
        }

        /// <summary>
        /// Returns list of groups for specified customer and range.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<IList<GroupResponseModel>> GetGroups(int? customerId, int skip, int take)
        {
            var groups = await this.groupsService.GetGroups(customerId, skip, take);

            return Mapper.Map<List<GroupResponseModel>>(groups);
        }

        /// <summary>
        /// Returns required group by is.
        /// Returns null if group with specified id not exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GroupResponseModel> GetGroupById(Guid id)
        {
            var group = await this.groupsService.GetGroupById(id);

            return Mapper.Map<GroupResponseModel>(group);
        }

        /// <summary>
        /// Creates new group in db.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public async Task<GroupResponseModel> CreateGroup(CreateGroupModel group)
        {
            var entity = Mapper.Map<Group>(group);
            var result = await this.groupsService.CreateGroup(entity);

            return Mapper.Map<GroupResponseModel>(result);
        }

        /// <summary>
        /// Updates existing group record.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public async Task<GroupResponseModel> UpdateGroup(Guid id, GroupModel group)
        {
            var entity = Mapper.Map<Group>(group);
            entity.Id = id;

            var result = await groupsService.UpdateGroup(entity);

            if (result != null)
            {
                foreach (var principal in result.Principals)
                {
                    await InvalidatePrincipalCachedData(principal.Id);
                }

                return Mapper.Map<GroupResponseModel>(result);
            }

            return null;
        }

        /// <summary>
        /// Deletes group with specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>False in case if group not exists.</returns>
        public async Task<bool> DeleteGroup(Guid id)
        {
            var group = await groupsService.GetGroupById(id);

            if (group != null)
            {
                await groupsService.DeleteGroup(group);

                foreach (var principal in group.Principals)
                {
                    await InvalidatePrincipalCachedData(principal.Id);
                }

                return true;
            }

            return false;
        }

        private async Task InvalidatePrincipalCachedData(Guid principalId)
        {
            var set = await redisCache.SetMembersAsync(principalId.ToString());

            foreach (var setMember in set)
            {
                await redisCache.KeyDeleteAsync(setMember.ToString());
            }

            await redisCache.KeyDeleteAsync(principalId.ToString());
        }
    }
}