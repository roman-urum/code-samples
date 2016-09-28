using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Dtos.Enums;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities.Enums;
using CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Results;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Interfaces
{
    public interface IPrincipalsService
    {
        /// <summary>
        /// Returns list of principals for specified customer and range.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IList<Principal>> GetPrincipals(int? customerId, int skip, int take);

        /// <summary>
        /// Returns required principal by id.
        /// Returns null if principal not exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Principal> GetPrincipalById(Guid id);

        /// <summary>
        /// Returns required principal by username.
        /// Returns null if principal not exists.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<Principal> GetPrincipalByUsername(string username);

        /// <summary>
        /// Creates record for new principal.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        Task<ServiceActionResult<CreatePrincipalStatus, Principal>> CreatePrincipal(Principal principal);

        /// <summary>
        /// Updates existing entity.
        /// Adds new credential value.
        /// Validates current credential value if specified.
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="credential"></param>
        /// <param name="currentCredentialValue"></param>
        /// <returns></returns>
        Task<ServiceActionResult<UpdatePrincipalStatus, Principal>> UpdatePrincipal(
            Principal principal,
            Credential credential, 
            string currentCredentialValue
        );

        /// <summary>
        /// Deletes principal from database.
        /// </summary>
        /// <returns>False if specified id not exists.</returns>
        Task<bool> DeletePrincipal(Guid id);

        /// <summary>
        /// Verifies credentials using provided value and returs result.
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="credentialType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<ServiceActionResult<ValidateCredentailsStatus, Credential>> VerifyCredentials(
            Principal principal,
            CredentialTypes credentialType,
            string value
        );
    }
}
