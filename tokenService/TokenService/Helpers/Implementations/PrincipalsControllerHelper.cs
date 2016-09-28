using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Dtos.Enums;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;
using CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Interfaces;
using CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Results;
using CareInnovations.HealthHarmony.Maestro.TokenService.Extensions;
using CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Interfaces;
using CareInnovations.HealthHarmony.Maestro.TokenService.Models;
using StackExchange.Redis;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Implementations
{
    /// <summary>
    /// PrincipalsControllerHelper.
    /// </summary>
    public class PrincipalsControllerHelper : IPrincipalsControllerHelper
    {
        private readonly IPrincipalsService principalsService;
        private readonly IGroupsService groupsService;
        private readonly IDatabase redisCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrincipalsControllerHelper" /> class.
        /// </summary>
        /// <param name="principalsService">The principals service.</param>
        /// <param name="groupsService">The groups service.</param>
        /// <param name="redisCache">The redis cache.</param>
        public PrincipalsControllerHelper(
            IPrincipalsService principalsService, 
            IGroupsService groupsService,
            IDatabase redisCache
        )
        {
            this.principalsService = principalsService;
            this.groupsService = groupsService;
            this.redisCache = redisCache;
        }

        /// <summary>
        /// Returns list of principals for specified customer and range.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<IList<PrincipalResponseModel>> GetPrincipals(int? customerId, int skip, int take)
        {
            var result = await this.principalsService.GetPrincipals(customerId, skip, take);

            return Mapper.Map<List<PrincipalResponseModel>>(result);
        }

        /// <summary>
        /// Returns required principal by id.
        /// Returns null if principal not exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PrincipalResponseModel> GetPrincipalById(Guid id)
        {
            var result = await this.principalsService.GetPrincipalById(id);

            if (result == null)
            {
                return null;
            }

            return Mapper.Map<PrincipalResponseModel>(result);
        }

        /// <summary>
        /// Creates record for new principal.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public async Task<ServiceActionResult<CreatePrincipalStatus, PrincipalResponseModel>> CreatePrincipal(CreatePrincipalModel principal)
        {
            var entity = Mapper.Map<Principal>(principal);

            if (principal.Groups != null && principal.Groups.Any())
            {
                entity.Groups = await this.groupsService.GetGroupsByIds(principal.Groups);
            }

            var result = await this.principalsService.CreatePrincipal(entity);

            if (result.Status != CreatePrincipalStatus.Success)
            {
                return result.Clone<PrincipalResponseModel>();
            }

            return result.CloneWithMapping(new PrincipalResponseModel());
        }

        /// <summary>
        /// Generates entity and updates principal data.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="principal"></param>
        /// <returns></returns>
        public async Task<ServiceActionResult<UpdatePrincipalStatus, PrincipalResponseModel>> UpdatePrincipal(
            Guid id,
            UpdatePrincipalModel principal
        )
        {
            var entity = Mapper.Map<Principal>(principal);
            var credential = Mapper.Map<Credential>(principal.Credential);

            entity.Id = id;
            entity.Groups = principal.Groups != null && principal.Groups.Any()
                ? await this.groupsService.GetGroupsByIds(principal.Groups)
                : new List<Group>();

            var result =
                await this.principalsService.UpdatePrincipal(entity, credential, principal.Credential != null ? principal.Credential.CurrentValue : null);

            if (result.Status != UpdatePrincipalStatus.Success)
            {
                return result.Clone<PrincipalResponseModel>();
            }

            await InvalidatePrincipalCachedData(id);

            return result.Clone(new PrincipalResponseModel());
        }

        /// <summary>
        /// Deletes principal from database.
        /// </summary>
        /// <returns>False if specified id not exists.</returns>
        public async Task<bool> DeletePrincipal(Guid id)
        {
            var result = await principalsService.DeletePrincipal(id);

            if (result)
            {
                await InvalidatePrincipalCachedData(id);
            }

            return result;
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