using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareInnovations.HealthHarmony.Maestro.TokenService.Common.Helpers;
using CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Dtos.Enums;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities.Enums;
using CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Interfaces;
using CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Results;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// PrincipalsService.
    /// </summary>
    public class PrincipalsService : IPrincipalsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Principal> principalsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrincipalsService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public PrincipalsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.principalsRepository = unitOfWork.CreateGenericRepository<Principal>();
        }

        /// <summary>
        /// Returns list of principals for specified customer and range.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<IList<Principal>> GetPrincipals(int? customerId, int skip, int take)
        {
            var result =
                await this.principalsRepository.FindPagedAsync(
                    x => (!customerId.HasValue || x.CustomerId == customerId.Value) && !x.IsDeleted,
                    principal => principal.OrderBy(p => p.Id), null, skip, take);

            return result.Results;
        }

        /// <summary>
        /// Returns required principal by id.
        /// Returns null if principal not exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Principal> GetPrincipalById(Guid id)
        {
            var result = await this.principalsRepository.FindAsync(p => p.Id == id && !p.IsDeleted);

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Returns required principal by username.
        /// Returns null if principal not exists.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<Principal> GetPrincipalByUsername(string username)
        {
            var result = await this.principalsRepository.FindAsync(p => p.Username.Equals(username) && !p.IsDeleted);

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Creates record for new principal.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public async Task<ServiceActionResult<CreatePrincipalStatus, Principal>> CreatePrincipal(Principal principal)
        {
            if (await this.principalsRepository.CountAsync(p => p.Username.Equals(principal.Username)) > 0)
            {
                return new ServiceActionResult<CreatePrincipalStatus, Principal>(CreatePrincipalStatus.DuplicateUsername);
            }

            principal.UpdatedUtc = DateTime.UtcNow;

            this.HashPasswordCredentials(principal);
            this.principalsRepository.Insert(principal);
            await this.unitOfWork.SaveAsync();

            return new ServiceActionResult<CreatePrincipalStatus, Principal>(CreatePrincipalStatus.Success, principal);
        }

        /// <summary>
        /// Updates existing entity.
        /// Adds new credential value.
        /// Validates current credential value if specified.
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="credential"></param>
        /// <param name="currentCredentialValue"></param>
        /// <returns></returns>
        public async Task<ServiceActionResult<UpdatePrincipalStatus, Principal>> UpdatePrincipal(
            Principal principal,
            Credential credential, 
            string currentCredentialValue
        )
        {
            var entity = await this.GetPrincipalById(principal.Id);

            if (entity == null)
            {
                return new ServiceActionResult<UpdatePrincipalStatus, Principal>(UpdatePrincipalStatus.NotFound);
            }

            if (await this.principalsRepository.CountAsync(
                p => p.Username.Equals(principal.Username) && p.Id != entity.Id) > 0)
            {
                return new ServiceActionResult<UpdatePrincipalStatus, Principal>(UpdatePrincipalStatus.DuplicateUsername);
            }

            if (credential != null)
            {
                var setCredentialResult = SetNewCredential(entity, credential, currentCredentialValue);

                if (setCredentialResult != UpdatePrincipalStatus.Success)
                {
                    return
                        new ServiceActionResult<UpdatePrincipalStatus, Principal>(
                            setCredentialResult);
                }
            }

            entity.Policies.Clear();
            entity.Groups.Clear();

            entity.FirstName = principal.FirstName;
            entity.LastName = principal.LastName;
            entity.Username = principal.Username;
            entity.Description = principal.Description;
            entity.Disabled = principal.Disabled;
            entity.ExpiresUtc = principal.ExpiresUtc;
            entity.Policies = principal.Policies;
            entity.Groups = principal.Groups;
            entity.UpdatedUtc = DateTime.UtcNow;

            this.principalsRepository.Update(entity);
            await this.unitOfWork.SaveAsync();

            return new ServiceActionResult<UpdatePrincipalStatus, Principal>(UpdatePrincipalStatus.Success, entity);
        }

        /// <summary>
        /// Deletes principal from database.
        /// </summary>
        /// <returns>False if specified id not exists.</returns>
        public async Task<bool> DeletePrincipal(Guid id)
        {
            var entity = await this.GetPrincipalById(id);

            if (entity == null)
            {
                return false;
            }

            this.principalsRepository.Delete(entity);
            await this.unitOfWork.SaveAsync();

            return true;
        }

        /// <summary>
        /// Verifies credentials using provided value and returs result.
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="credentialType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<ServiceActionResult<ValidateCredentailsStatus, Credential>> VerifyCredentials(
            Principal principal,
            CredentialTypes credentialType,
            string value
        )
        {
            if (principal == null || principal.Disabled)
            {
                return new ServiceActionResult<ValidateCredentailsStatus, Credential>(ValidateCredentailsStatus.Invalid);
            }

            var matchCredential = this.GetCredential(principal, credentialType, value);

            Credential result;
            ValidateCredentailsStatus status;

            if (matchCredential != null && (matchCredential.ExpiresUtc == null || matchCredential.ExpiresUtc > DateTime.UtcNow))
            {
                principal.FailedCount = 0;
                result = matchCredential;
                status = ValidateCredentailsStatus.Valid;
            }
            else
            {
                principal.FailedCount++;
                result = null;
                status = ValidateCredentailsStatus.Invalid;
            }

            this.principalsRepository.Update(principal);
            await unitOfWork.SaveAsync();

            return new ServiceActionResult<ValidateCredentailsStatus, Credential>(status, result);
        }

        #region Private methods

        /// <summary>
        /// Generates hash for all credentials with type Password.
        /// </summary>
        /// <param name="principal"></param>
        private void HashPasswordCredentials(Principal principal)
        {
            if (principal.Credentials != null)
            {
                foreach (var cred in principal.Credentials.Where(c => c.Type == CredentialTypes.Password))
                {
                    cred.Value = HashGenerator.GetHash(cred.Value);
                }
            }
        }

        /// <summary>
        /// Sets new value of new credential.
        /// Verifies current credential if specified.
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="credential"></param>
        /// <param name="currentCredentialValue"></param>
        /// <returns>False in case if current credential value is not matches.</returns>
        private UpdatePrincipalStatus SetNewCredential(Principal principal, Credential credential, string currentCredentialValue)
        {
            if (!string.IsNullOrEmpty(currentCredentialValue))
            {
                var credentialEntity = this.GetCredential(principal, credential.Type, currentCredentialValue);

                if (credentialEntity == null)
                {
                    return UpdatePrincipalStatus.InvalidCurrentCredentialValue;
                }
            }

            if (credential.Type == CredentialTypes.Password)
            {
                if (principal.Credentials.Any(
                        c => c.Type == CredentialTypes.Password && ModularCrypt.Verify(credential.Value, c.Value)))
                {
                    return UpdatePrincipalStatus.CredentialAlreadyUsed;
                }

                credential.Value = HashGenerator.GetHash(credential.Value);
            }

            foreach (var currentCredential in principal.Credentials.Where(c=>c.Type == credential.Type && !c.Disabled))
            {
                currentCredential.Disabled = true;
            }

            principal.Credentials.Add(credential);

            return UpdatePrincipalStatus.Success;
        }

        /// <summary>
        /// Returns credential from principal matches to specified conditions.
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="credentialType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private Credential GetCredential(Principal principal, CredentialTypes credentialType, string value)
        {
            var creds = principal.Credentials.Where(c =>
                c.Disabled == false &&
                c.Type == credentialType).ToList();

            return credentialType == CredentialTypes.Password
                ? creds.FirstOrDefault(c => ModularCrypt.Verify(value, c.Value))
                : creds.FirstOrDefault(c => c.Value == value);
        }

        #endregion
    }
}