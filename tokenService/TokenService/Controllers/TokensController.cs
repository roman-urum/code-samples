using System;
using CareInnovations.HealthHarmony.Maestro.TokenService.Models;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using CareInnovations.HealthHarmony.Maestro.TokenService.Common.Extensions;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Dtos.Enums;
using CareInnovations.HealthHarmony.Maestro.TokenService.Filters;
using CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Interfaces;
using CareInnovations.HealthHarmony.Maestro.TokenService.Models.Enums;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Controllers
{
    /// <summary>
    /// TokensController.
    /// </summary>
    public class TokensController : ServiceController
    {
        private readonly ITokensControllerHelper tokensControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokensController" /> class.
        /// </summary>
        public TokensController(ITokensControllerHelper tokensControllerHelper)
        {
            this.tokensControllerHelper = tokensControllerHelper;
        }

        /// <summary>
        /// Verify access for provided access token (id).
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="action">The action.</param>
        /// <param name="service">The service.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="customer">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [SkipModelValidation]
        [Route(@"api/Tokens/{id}")]
        public async Task<IHttpActionResult> VerifyAccess(
            string id,
            string action,
            string service,
            string controller,
            int? customer = null, 
            Guid? patientId = null
        )
        {
            var result = await tokensControllerHelper
                .VerifyAccess(
                    id,
                    action,
                    service, 
                    controller, 
                    customer,
                    patientId
                );

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Generate access token.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route(@"api/Tokens")]
        [LogIpAddresses]
        public async Task<IHttpActionResult> CreateTokenByCredentials(TokenRequestModel model)
        {
            var result = await tokensControllerHelper.CreateTokenByCredentials(model);

            if (result.Status != ValidateCredentailsStatus.Valid)
            {
                return Content(
                    HttpStatusCode.Unauthorized,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.Status.GetConcatString()
                    }
                );
            }

            return Ok(result.Content);
        }

        /// <summary>
        /// Generates the token by certificate.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route(@"api/CertificateTokens")]
        [LogIpAddresses]
        public async Task<IHttpActionResult> CreateTokenByCertificate(TokenByCertificateRequestModel model)
        {
            var result = await tokensControllerHelper.CreateTokenByCertificate(model);

            if (result.Status != ValidateCredentailsStatus.Valid)
            {
                return Content(
                    HttpStatusCode.Unauthorized,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.Status.GetConcatString()
                    }
                );
            }

            return Ok(result.Content);
        }

        /// <summary>
        /// Delete a token (e.g. user logs out)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(@"api/Tokens/{id}")]
        public async Task<IHttpActionResult> DeleteToken(string id)
        {
            var deleted = await tokensControllerHelper.DeleteToken(id);

            if (deleted)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            
            return NotFound();
        }
    }
}