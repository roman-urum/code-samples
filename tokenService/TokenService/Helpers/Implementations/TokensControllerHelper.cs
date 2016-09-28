using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Dtos.Enums;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities.Enums;
using CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Interfaces;
using CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Results;
using CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Interfaces;
using CareInnovations.HealthHarmony.Maestro.TokenService.Models;
using Newtonsoft.Json;
using NLog;
using StackExchange.Redis;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Implementations
{
    /// <summary>
    /// TokensControllerHelper.
    /// </summary>
    public class TokensControllerHelper : ITokensControllerHelper
    {
        private const int MaestroAdminsCustomerId = 1;
        private const string ValidAuthenticationAttemptLogFormat = "Authentication attempt: UserName - {0}, " +
            "Service - {1}, Controller - {2}, Action - {3}, CustomerId - {4}, Allowed - {5}";
        private const string DevicePrincipalName = "DevicePrincipal";

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IDatabase redisCache;
        private readonly IPrincipalsService principalsService;
        private readonly ICertificatesService certificatesService;
        private readonly IGroupsService groupsService;

        private static readonly TimeSpan TOKEN_TTL = TimeSpan.FromMinutes(720);

        /// <summary>
        /// Initializes a new instance of the <see cref="TokensControllerHelper" /> class.
        /// </summary>
        /// <param name="redisCache">The redis cache.</param>
        /// <param name="principalsService">The principals service.</param>
        /// <param name="certificatesService">The certificates service.</param>
        public TokensControllerHelper(
            IDatabase redisCache,
            IPrincipalsService principalsService,
            ICertificatesService certificatesService
        )
        {
            this.redisCache = redisCache;
            this.principalsService = principalsService;
            this.certificatesService = certificatesService;
        }

        /// <summary>
        /// Verifies if provided id(token) has access to specified functionality
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="action">The action.</param>
        /// <param name="service">The service.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="customer">The customer.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<VerifyAccessResponseModel> VerifyAccess(
            string id,
            string action,
            string service,
            string controller,
            int? customer = null,
            Guid? patientId = null
        )
        {
            var cached = await redisCache.StringGetAsync(id);

            if (cached.IsNull)
            {
                return null;
            }

            var cpm = JsonConvert.DeserializeObject<CachedPrincipalModel>(cached.ToString());

            redisCache.KeyExpire(id, TOKEN_TTL, CommandFlags.FireAndForget);

            var allowed = !cpm.UserName.Equals(DevicePrincipalName) ?
                VerifyPolicies(action, service, controller, cpm, customer) :
                VerifyDevicePrincipalPolicies(action, service, controller, cpm, customer, patientId);

            logger.Info(
                ValidAuthenticationAttemptLogFormat,
                cpm.UserName,
                service,
                controller,
                action,
                customer,
                allowed
            );

            if (allowed)
            {
                return new VerifyAccessResponseModel()
                {
                    Id = id,
                    Allowed = true,
                    TTL = TOKEN_TTL.TotalSeconds,
                    PrincipalId = cpm.PrincipalId,
                    FullName = cpm.FullName
                };
            }

            return null;
        }

        /// <summary>
        /// Verifies credentials and generates response model with token.
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceActionResult<ValidateCredentailsStatus, TokenResponseModel>> CreateTokenByCredentials(
            TokenRequestModel model
        )
        {
            var principal = await this.principalsService.GetPrincipalByUsername(model.Username);

            var validationResult = string.IsNullOrEmpty(model.Password) ?
                await this.principalsService.VerifyCredentials(principal, CredentialTypes.ApiKey, model.ApiKey) :
                await this.principalsService.VerifyCredentials(principal, CredentialTypes.Password, model.Password);

            if (validationResult.Status != ValidateCredentailsStatus.Valid)
            {
                return validationResult.Clone<TokenResponseModel>();
            }

            var response = new TokenResponseModel
            {
                Id = TokensGenerator.GetToken(),
                Success = true,
                TTL = (int)TOKEN_TTL.TotalSeconds,
                ExpirationUtc = validationResult.Content.ExpiresUtc
            };

            await CachePolicies(response.Id, principal, principal.Groups.ToList(), TOKEN_TTL);

            return validationResult.Clone(response);
        }

        /// <summary>
        /// Creates the token by certificate.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<ServiceActionResult<ValidateCredentailsStatus, TokenResponseModel>> CreateTokenByCertificate(
            TokenByCertificateRequestModel model
        )
        {
            if (!model.CustomerId.HasValue)
            {
                throw new ArgumentNullException("customerId");
            }

            if (!model.PatientId.HasValue)
            {
                throw new ArgumentNullException("patientId");
            }

            var verified = await certificatesService.VerifySignature(
                model.CustomerId.Value, 
                model.PatientId.Value, 
                Convert.FromBase64String(model.NonceBase64), 
                Convert.FromBase64String(model.SignatureBase64)
            );

            if (!verified)
            {
                return new ServiceActionResult<ValidateCredentailsStatus, TokenResponseModel>(
                    ValidateCredentailsStatus.Invalid
                );
            }

            var principal = await principalsService.GetPrincipalByUsername(DevicePrincipalName);

            if (principal == null)
            {
                return new ServiceActionResult<ValidateCredentailsStatus, TokenResponseModel>(
                    ValidateCredentailsStatus.Invalid
                );
            }

            var response = new TokenResponseModel()
            {
                Id = TokensGenerator.GetToken(),
                Success = true,
                TTL = (int)TOKEN_TTL.TotalSeconds,
                ExpirationUtc = null
            };

            await CacheDevicePrincipalPolicies(response.Id, model.CustomerId.Value, model.PatientId.Value, principal, TOKEN_TTL);

            return new ServiceActionResult<ValidateCredentailsStatus, TokenResponseModel>(
                ValidateCredentailsStatus.Valid,
                response
            );
        }

        /// <summary>
        /// Deletes token.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteToken(string id)
        {
            return await redisCache.KeyDeleteAsync(id);
        }

        #region Private methods

        /// <summary>
        /// Caches the policies.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="principal">The user.</param>
        /// <param name="groups">The groups.</param>
        /// <param name="ttl">The TTL.</param>
        /// <returns></returns>
        private async Task<CachedPrincipalModel> CachePolicies(
            string key,
            Principal principal,
            IList<Group> groups,
            TimeSpan ttl
        )
        {
            var cached = new CachedPrincipalModel
            {
                PrincipalId = principal.Id,
                UpdatedUtc = principal.UpdatedUtc.Ticks,
                CustomerId = principal.CustomerId,
                UserName = principal.Username,
                FullName = principal.FullName
            };

            cached.Policies.AddRange(Mapper.Map<IList<CachedPolicyModel>>(principal.Policies));

            foreach (var group in groups)
            {
                cached.Policies.AddRange(Mapper.Map<IList<CachedPolicyModel>>(group.Policies));
            }

            var principalTokenAdded = await redisCache.StringSetAsync(key, JsonConvert.SerializeObject(cached), ttl);
            
            if (principalTokenAdded)
            {
                // Storing all cached tokens related to particular Principal under the same Set with key == PrincipalId
                await redisCache.SetAddAsync(cached.PrincipalId.ToString(), key);
            }

            return cached;
        }

        /// <summary>
        /// Caches the device principal policies.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="principal">The principal.</param>
        /// <param name="ttl">The TTL.</param>
        /// <returns></returns>
        private async Task<CachedPrincipalModel> CacheDevicePrincipalPolicies(
            string key,
            int customerId,
            Guid patientId,
            Principal principal,
            TimeSpan ttl
        )
        {
            var cached = new CachedPrincipalModel
            {
                PrincipalId = principal.Id,
                UpdatedUtc = principal.UpdatedUtc.Ticks,
                CustomerId = customerId,
                UserName = principal.Username,
                FullName = principal.FullName,
                PatientId = patientId
            };

            cached.Policies.AddRange(Mapper.Map<IList<CachedPolicyModel>>(principal.Policies));

            var principalTokenAdded = await redisCache.StringSetAsync(key, JsonConvert.SerializeObject(cached), ttl);

            if (principalTokenAdded)
            {
                // Storing all cached tokens related to particular Principal under the same Set with key == PrincipalId
                await redisCache.SetAddAsync(cached.PrincipalId.ToString(), key);
            }

            return cached;
        }

        /// <summary>
        /// Verifies the policies.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="service">The service.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="cpm">The CPM.</param>
        /// <param name="customer">The customer.</param>
        /// <returns></returns>
        private bool VerifyPolicies(
            string action,
            string service,
            string controller,
            CachedPrincipalModel cpm,
            int? customer = null
        )
        {
            var act = ActionsMapper.Actions[action.ToLower()];

            var allPolicies = cpm.Policies.Where(p =>
                ((int) p.Action & act) != 0 &&
                (p.Service.Equals(service, StringComparison.InvariantCultureIgnoreCase) ||
                p.Service.Equals("*", StringComparison.InvariantCultureIgnoreCase)) &&
                (p.Controller.Equals(controller, StringComparison.InvariantCultureIgnoreCase) ||
                 p.Controller.Equals("*", StringComparison.InvariantCultureIgnoreCase)))
                .ToList();

            // If customer id is missed from URL we need to default it to "1" and verify access accordingly.
            // Only super admins shall have access to the services that do not have customer id.
            if (!customer.HasValue)
            {
                if (cpm.CustomerId == MaestroAdminsCustomerId)
                {
                    return allPolicies.Any() && allPolicies.All(p => p.Effect == PolicyEffects.Allow);
                }

                return false;
            }

            // customer has value but all rules are without customerId
            if (allPolicies.Any() && allPolicies.All(p => !p.Customer.HasValue && p.Effect == PolicyEffects.Allow))
            {
                // then current customer's user (except Maestro Admins) must have access only to his customer
                return cpm.CustomerId == MaestroAdminsCustomerId || cpm.CustomerId == customer.Value;
            }

            // specific policies if one customer's user needs access to another customer.
            var specificPolicies = allPolicies
                .Where(p => p.Customer.HasValue && p.Customer.Value == customer.Value)
                .ToList();

            return specificPolicies.Any() &&
                specificPolicies.All(p => p.Effect == PolicyEffects.Allow);
        }

        /// <summary>
        /// Verifies the device principal policies.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="service">The service.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="cpm">The CPM.</param>
        /// <param name="customer">The customer.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        private bool VerifyDevicePrincipalPolicies(
            string action,
            string service,
            string controller,
            CachedPrincipalModel cpm,
            int? customer,
            Guid? patientId
        )
        {
            if (customer.HasValue)
            {
                var act = ActionsMapper.Actions[action.ToLower()];

                var allPolicies = cpm.Policies.Where(p =>
                    ((int)p.Action & act) != 0 &&
                    p.Service.Equals(service, StringComparison.InvariantCultureIgnoreCase) &&
                    (p.Controller.Equals(controller, StringComparison.InvariantCultureIgnoreCase) ||
                     p.Controller.Equals("*", StringComparison.InvariantCultureIgnoreCase)))
                    .ToList();

                if (allPolicies.Any() && allPolicies.All(p => !p.Customer.HasValue && p.Effect == PolicyEffects.Allow))
                {
                    if (patientId.HasValue)
                    {
                        return cpm.CustomerId == customer.Value && cpm.PatientId == patientId.Value;
                    }

                    return cpm.CustomerId == customer.Value;
                }
            }

            return false;
        }

        #endregion
    }
}