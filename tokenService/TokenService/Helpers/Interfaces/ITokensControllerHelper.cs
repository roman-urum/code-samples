using System;
using System.Threading.Tasks;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Dtos.Enums;
using CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Results;
using CareInnovations.HealthHarmony.Maestro.TokenService.Models;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Interfaces
{
    /// <summary>
    /// ITokensControllerHelper.
    /// </summary>
    public interface ITokensControllerHelper
    {
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
        Task<VerifyAccessResponseModel> VerifyAccess(
            string id,
            string action,
            string service, 
            string controller,
            int? customer = null,
            Guid? patientId = null
        );

        /// <summary>
        /// Verifies credentials and generates response model with token.
        /// </summary>
        /// <returns></returns>
        Task<ServiceActionResult<ValidateCredentailsStatus, TokenResponseModel>> CreateTokenByCredentials(
            TokenRequestModel model
        );

        /// <summary>
        /// Deletes token.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteToken(string id);

        /// <summary>
        /// Creates the token by certificate.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<ServiceActionResult<ValidateCredentailsStatus, TokenResponseModel>> CreateTokenByCertificate(
            TokenByCertificateRequestModel model
        );
    }
}
