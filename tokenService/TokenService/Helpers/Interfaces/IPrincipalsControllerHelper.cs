using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Dtos.Enums;
using CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Results;
using CareInnovations.HealthHarmony.Maestro.TokenService.Models;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Interfaces
{
    /// <summary>
    /// IPrincipalsControllerHelper.
    /// </summary>
    public interface IPrincipalsControllerHelper
    {
        /// <summary>
        /// Returns list of principals for specified customer and range.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IList<PrincipalResponseModel>> GetPrincipals(int? customerId, int skip, int take);

        /// <summary>
        /// Returns required principal by id.
        /// Returns null if principal not exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PrincipalResponseModel> GetPrincipalById(Guid id);

        /// <summary>
        /// Creates record for new principal.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        Task<ServiceActionResult<CreatePrincipalStatus, PrincipalResponseModel>> CreatePrincipal(CreatePrincipalModel principal);

        /// <summary>
        /// Generates entity and updates principal data.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="principal"></param>
        /// <returns></returns>
        Task<ServiceActionResult<UpdatePrincipalStatus, PrincipalResponseModel>> UpdatePrincipal(Guid id,
            UpdatePrincipalModel principal);

        /// <summary>
        /// Deletes principal from database.
        /// </summary>
        /// <returns>False if specified id not exists.</returns>
        Task<bool> DeletePrincipal(Guid id);
    }
}
