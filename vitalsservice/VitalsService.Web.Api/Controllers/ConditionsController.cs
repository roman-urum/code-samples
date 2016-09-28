using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Swashbuckle.Swagger.Annotations;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Extensions;
using VitalsService.Web.Api.Filters;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.Conditions;
using VitalsService.Web.Api.Models.Enums;

namespace VitalsService.Web.Api.Controllers
{
    /// <summary>
    /// OrganizationsController.
    /// </summary>
    [TokenAuthorize]
    [RoutePrefix(@"api/{customerId:regex(^[1-9]\d*)}/conditions")]
    public class ConditionsController : ApiController
    {
        private readonly IConditionsControllerHelper conditionsControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionsController" /> class.
        /// </summary>
        /// <param name="conditionsControllerHelper"></param>
        public ConditionsController(IConditionsControllerHelper conditionsControllerHelper)
        {
            this.conditionsControllerHelper = conditionsControllerHelper;
        }

        /// <summary>
        /// Gets the condition.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="conditionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{conditionId:guid}")]
        [ResponseType(typeof(ConditionResponseDto))]
        [SwaggerResponse(HttpStatusCode.OK, "Existing customer condition.", typeof(ConditionResponseDto))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Customer condition with such identifier does not exist.")]
        public async Task<IHttpActionResult> GetCondition(int customerId, Guid conditionId)
        {
            var getConditionResult = await conditionsControllerHelper.GetCondition(customerId, conditionId);

            if (!getConditionResult.Status.HasFlag(ConditionStatus.Success))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = getConditionResult.Status.Description()
                });
            }

            return Ok(getConditionResult.Content);
        }

        /// <summary>
        /// Gets the list of conditions.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(PagedResultDto<ConditionResponseDto>))]
        [SwaggerResponse(HttpStatusCode.OK, "Response with customer conditions.", typeof(PagedResultDto<ConditionResponseDto>))]
        public async Task<IHttpActionResult> GetConditions(int customerId, [FromUri]ConditionSearchDto searchRequest)
        {
            var response = await conditionsControllerHelper.GetConditions(customerId, searchRequest);
            return Ok(response);
        }

        /// <summary>
        /// Creates new condition.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        [InvalidateCacheOutput(nameof(GetConditions), typeof(ConditionsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetCondition), typeof(ConditionsController), "customerId")]
        public async Task<IHttpActionResult> CreateCondition(int customerId, ConditionRequestDto request)
        {
            var createConditionResult = await conditionsControllerHelper.CreateCondition(customerId, request);

            if (!createConditionResult.Status.HasFlag(ConditionStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = createConditionResult.Status.Description()
                    }
                );
            }

            return Created(
                new Uri(Request.RequestUri, createConditionResult.Content.ToString()),
                new PostResponseDto<Guid>() { Id = createConditionResult.Content }
            );
        }

        /// <summary>
        /// Updates existing condition.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="conditionId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{conditionId:guid}")]
        [InvalidateCacheOutput(nameof(GetConditions), typeof(ConditionsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetCondition), typeof(ConditionsController), "customerId", "conditionId")]
        [SwaggerResponse(HttpStatusCode.OK, "Customer condition was updated.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Customer consition does not exist.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in the request.")]
        public async Task<IHttpActionResult> UpdateCondition(
            int customerId,
            Guid conditionId,
            ConditionRequestDto request
        )
        {
            var updateConditionStatus = await conditionsControllerHelper.UpdateCondition(customerId, conditionId, request);

            if (updateConditionStatus.HasFlag(ConditionStatus.NotFound))
            {
                return Content(
                    HttpStatusCode.NotFound, 
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = updateConditionStatus.Description()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes existing condition.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="conditionId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{conditionId:guid}")]
        [InvalidateCacheOutput("GetDefaultThresholds", typeof(DefaultThresholdsController), "customerId")]
        [InvalidateCacheOutput("GetDefaultThreshold", typeof(DefaultThresholdsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetConditions), typeof(ConditionsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetCondition), typeof(ConditionsController), "customerId", "conditionId")]
        [InvalidateCacheOutput(nameof(PatientConditionsController.GetPatientConditions), typeof(PatientConditionsController), "customerId")]
        [SwaggerResponse(HttpStatusCode.OK, "Customer condition was removed.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Customer condition with such identifier does not exist.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in the request.")]
        public async Task<IHttpActionResult> DeleteCondition(int customerId, Guid conditionId)
        {
            var deleteStatus = await conditionsControllerHelper.DeleteCondition(customerId, conditionId);

            if (deleteStatus.HasFlag(ConditionStatus.NotFound))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = deleteStatus.Description()
                    });
            }

            return StatusCode(HttpStatusCode.NoContent);
        } 
    }
}