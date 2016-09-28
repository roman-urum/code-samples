using CareInnovations.HealthHarmony.Maestro.TokenService.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using CareInnovations.HealthHarmony.Maestro.TokenService.Common.Extensions;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Dtos.Enums;
using CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Interfaces;
using CareInnovations.HealthHarmony.Maestro.TokenService.Models.Enums;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Controllers
{
    /// <summary>
    /// PrincipalsController.
    /// </summary>
    public class PrincipalsController : ServiceController
    {
        private readonly IPrincipalsControllerHelper principalsControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrincipalsController" /> class.
        /// </summary>
        /// <param name="principalsControllerHelper">The principals controller helper.</param>
        public PrincipalsController(IPrincipalsControllerHelper principalsControllerHelper)
        {
            this.principalsControllerHelper = principalsControllerHelper;
        }

        /// <summary>
        /// Gets principals.
        /// </summary>
        /// <param name="take">The take.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get(
            int take = MAX_PAGE_SIZE,
            int skip = 0,
            int? customerId = null
        )
        {
            var result =
                await this.principalsControllerHelper.GetPrincipals(customerId, skip, Math.Min(take, MAX_PAGE_SIZE));

            return Ok(result);
        }

        /// <summary>
        /// Gets the specified principal.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var principal = await this.principalsControllerHelper.GetPrincipalById(id);

            if (principal != null)
            {
                return Ok(principal);
            }

            return NotFound();
        }

        /// <summary>
        /// Creates the specified principal.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Object should not have an ID.</exception>
        public async Task<IHttpActionResult> Post(CreatePrincipalModel model)
        {
            var result = await this.principalsControllerHelper.CreatePrincipal(model);

            if (result.Status == CreatePrincipalStatus.DuplicateUsername)
            {
                return StatusCode(HttpStatusCode.Conflict);
            }

            return Ok(result.Content);
        }

        /// <summary>
        /// Updates the specified principal.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Object has an ID and it doesn't match the resource ID.</exception>
        public async Task<IHttpActionResult> Put(Guid id, UpdatePrincipalModel model)
        {
            var result = await this.principalsControllerHelper.UpdatePrincipal(id, model);

            switch (result.Status)
            {
                case UpdatePrincipalStatus.NotFound:
                    return NotFound();

                case UpdatePrincipalStatus.DuplicateUsername:
                    return StatusCode(HttpStatusCode.Conflict);

                case UpdatePrincipalStatus.InvalidCurrentCredentialValue:
                    return Content(
                        HttpStatusCode.BadRequest,
                        new ErrorResponseDto()
                        {
                            Error = ErrorCode.InvalidCredentialValue,
                            Message = ErrorCode.InvalidCredentialValue.Description(),
                            Details = result.Status.GetConcatString()
                        }
                    );

                case UpdatePrincipalStatus.CredentialAlreadyUsed:
                    return Content(
                        HttpStatusCode.BadRequest,
                        new ErrorResponseDto()
                        {
                            Error = ErrorCode.CredentialAlreadyUsed,
                            Message = ErrorCode.CredentialAlreadyUsed.Description(),
                            Details = result.Status.GetConcatString()
                        }
                    );

                case UpdatePrincipalStatus.Success:
                    return StatusCode(HttpStatusCode.NoContent);

                default:
                    return BadRequest();
            }
        }

        /// <summary>
        /// Deletes the specified principal.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            if (await principalsControllerHelper.DeletePrincipal(id))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return NotFound();
        }
    }
}