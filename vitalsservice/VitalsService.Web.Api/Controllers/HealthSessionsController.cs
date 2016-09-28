using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Extensions;
using VitalsService.Web.Api.Filters;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.Enums;
using VitalsService.Web.Api.Models.HealthSessions;

namespace VitalsService.Web.Api.Controllers
{
    /// <summary>
    /// Controller to manage health sessions data.
    /// </summary>
    [TokenAuthorize]
    [RoutePrefix(@"api/{customerId:regex(^[1-9]\d*)}/sessions/{patientId:guid}")]
    public class HealthSessionsController : ApiController
    {
        private readonly IHealthSessionsControllerHelper healthSessionHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthSessionsController"/> class.
        /// </summary>
        /// <param name="healthSessionHelper">The health session helper.</param>
        public HealthSessionsController(IHealthSessionsControllerHelper healthSessionHelper)
        {
            this.healthSessionHelper = healthSessionHelper;
        }

        /// <summary>
        /// Creates new health session.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [CertificateAuthorize]
        [SwaggerResponse(HttpStatusCode.OK, "Health session is saved.", typeof(PostResponseDto<Guid>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrect data provided in request.")]
        [InvalidateCacheOutput(nameof(GetHealthSessions), typeof(HealthSessionsController), "customerId", "patientId")]
        [InvalidateCacheOutput(nameof(GetHealthSession), typeof(HealthSessionsController), "customerId", "patientId")]
        public async Task<IHttpActionResult> CreateHealthSession(
            HealthSessionRequestDto model,
            int customerId,
            Guid patientId
        )
        {
            var operationResult = await healthSessionHelper.Create(model, customerId, patientId);

            if (operationResult.Status.HasFlag(CreateHealthSessionStatus.HealthSessionWithClientIdAlreadyExists))
            {
                return Content(
                    HttpStatusCode.Conflict,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = $"{operationResult.Status.Description()} (provided ClientId: {model.ClientId})"
                    }
                );
            }

            if (!operationResult.Status.HasFlag(CreateHealthSessionStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.Status.Description()
                    }
                );
            }

            return Ok(operationResult.Content);
        }
        
        /// <summary>
        /// Updates existing health session.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [CertificateAuthorize]
        [Route("{healthSessionId:guid}")]
        [SwaggerResponse(HttpStatusCode.OK, "Health session is updated.", typeof(PostResponseDto<Guid>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrect data provided in request.")]
        [InvalidateCacheOutput(nameof(GetHealthSessions), typeof(HealthSessionsController), "customerId", "patientId")]
        [InvalidateCacheOutput(nameof(GetHealthSession), typeof(HealthSessionsController), "customerId", "patientId", "healthSessionId")]
        public async Task<IHttpActionResult> UpdateHealthSession(
            UpdateHealthSessionRequestDto model,
            int customerId,
            Guid patientId,
            Guid healthSessionId
        )
        {
            var operationResult = await healthSessionHelper.Update(model, customerId, patientId, healthSessionId);

            if (!operationResult.HasFlag(UpdateHealthSessionStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.Description()
                    }
                );
            }

            return Ok(operationResult);
        }

        /// <summary>
        /// Returns all saved health sessions for patient.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, "Returns response with list of sessions.", typeof(PagedResultDto<HealthSessionResponseDto>))]
        public async Task<IHttpActionResult> GetHealthSessions(
            int customerId,
            Guid patientId,
            [FromUri]HealthSessionsSearchDto searchDto
        )
        {
            var result = await healthSessionHelper.Find(customerId, patientId, searchDto);

            return Ok(result);
        }

        /// <summary>
        /// Returns health session info by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="healthSessionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{healthSessionId:guid}")]
        [SwaggerResponse(HttpStatusCode.OK, "Required health session exists.", typeof(HealthSessionResponseDto))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Required health session not exists.")]
        public async Task<IHttpActionResult> GetHealthSession(
            int customerId,
            Guid patientId,
            Guid healthSessionId
        )
        {
            var operationResult = await healthSessionHelper.GetById(customerId, patientId, healthSessionId);

            if (operationResult.Status.HasFlag(GetHealthSessionStatus.HealthSessionNotFound))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.Status.Description()
                    }
                );
            }

            if (!operationResult.Status.HasFlag(GetHealthSessionStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.Status.Description()
                    }
                );
            }

            return Ok(operationResult.Content);
        }
    }
}