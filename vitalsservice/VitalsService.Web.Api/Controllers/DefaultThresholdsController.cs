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
using VitalsService.Web.Api.Models.Enums;
using VitalsService.Web.Api.Models.Thresholds;

namespace VitalsService.Web.Api.Controllers
{
    /// <summary>
    /// DefaultThresholdsController.
    /// </summary>
    [TokenAuthorize]
    [RoutePrefix(@"api/{customerId:regex(^[1-9]\d*)}/thresholds/defaults")]
    public class DefaultThresholdsController : ApiController
    {
        private readonly IDefaultThresholdsControllerHelper defaultThresholdsControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThresholdsController" /> class.
        /// </summary>
        /// <param name="defaultThresholdsControllerHelper">The default thresholds controller helper.</param>
        public DefaultThresholdsController(IDefaultThresholdsControllerHelper defaultThresholdsControllerHelper)
        {
            this.defaultThresholdsControllerHelper = defaultThresholdsControllerHelper;
        }

        /// <summary>
        /// Creates the default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        [InvalidateCacheOutput(nameof(GetDefaultThresholds), typeof(DefaultThresholdsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetDefaultThreshold), typeof(DefaultThresholdsController), "customerId")]
        [InvalidateCacheOutput("GetThresholds", typeof(ThresholdsController), "customerId")]
        public async Task<IHttpActionResult> CreateDefaultThreshold(
            int customerId,
            DefaultThresholdRequestDto request
        )
        {
            var result = await defaultThresholdsControllerHelper.CreateDefaultThreshold(customerId, request);

            if (!result.Status.HasFlag(CreateUpdateDefaultThresholdStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.Status.GetConcatString()
                    }
                );
            }

            return Created(
                new Uri(Request.RequestUri, result.Content.ToString()),
                new PostResponseDto<Guid> { Id = result.Content }
            );
        }

        /// <summary>
        /// Updates the default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="defaultThresholdId">The default threshold identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{defaultThresholdId:guid}")]
        [SwaggerResponse(HttpStatusCode.OK, "Default threshold was updated.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Default threshold does not exist.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in the request.")]
        [InvalidateCacheOutput(nameof(GetDefaultThresholds), typeof(DefaultThresholdsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetDefaultThreshold), typeof(DefaultThresholdsController), "customerId", "defaultThresholdId")]
        [InvalidateCacheOutput("GetThresholds", typeof(ThresholdsController), "customerId")]
        [InvalidateCacheOutput("GetVital", typeof(VitalsController), "customerId")]
        [InvalidateCacheOutput("GetVitals", typeof(VitalsController), "customerId")]
        public async Task<IHttpActionResult> UpdateDefaultThreshold(
            int customerId,
            Guid defaultThresholdId,
            DefaultThresholdRequestDto request
        )
        {
            var status = await defaultThresholdsControllerHelper.UpdateDefaultThreshold(customerId, defaultThresholdId, request);

            if (!status.HasFlag(CreateUpdateDefaultThresholdStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = status.GetConcatString()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes the default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="defaultThresholdId">The default threshold identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{defaultThresholdId:guid}")]
        [SwaggerResponse(HttpStatusCode.OK, "Default threshold was deleted.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Default threshold with such identifier does not exist.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in the request.")]
        [InvalidateCacheOutput(nameof(GetDefaultThresholds), typeof(DefaultThresholdsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetDefaultThreshold), typeof(DefaultThresholdsController), "customerId", "defaultThresholdId")]
        [InvalidateCacheOutput("GetThresholds", typeof(ThresholdsController), "customerId")]
        [InvalidateCacheOutput("GetVital", typeof(VitalsController), "customerId")]
        [InvalidateCacheOutput("GetVitals", typeof(VitalsController), "customerId")]
        public async Task<IHttpActionResult> DeleteDefaultThreshold(
            int customerId,
            Guid defaultThresholdId
        )
        {
            var status = await defaultThresholdsControllerHelper.DeleteDefaultThreshold(customerId, defaultThresholdId);

            if (status == GetDeleteDefaultThresholdStatus.NotFound)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = status.Description()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Gets the default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="defaultThresholdId">The default threshold identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{defaultThresholdId:guid}")]
        [ResponseType(typeof(DefaultThresholdDto))]
        [SwaggerResponse(HttpStatusCode.OK, "Existing default threshold.", typeof(DefaultThresholdDto))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Default threshold does not exist.")]
        public async Task<IHttpActionResult> GetDefaultThreshold(
            int customerId,
            Guid defaultThresholdId
        )
        {
            var result = await defaultThresholdsControllerHelper.GetDefaultThreshold(customerId, defaultThresholdId);

            if (result.Status == GetDeleteDefaultThresholdStatus.NotFound)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.Status.Description()
                    }
                );
            }

            return Ok(result.Content);
        }

        /// <summary>
        /// Gets the default thresholds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(PagedResultDto<DefaultThresholdDto>))]
        [SwaggerResponse(HttpStatusCode.OK, "Response with default thresholds.", typeof(PagedResultDto<DefaultThresholdDto>))]
        public async Task<IHttpActionResult> GetDefaultThresholds(
            int customerId,
            [FromUri]DefaultThresholdsSearchDto request
        )
        {
            var result = await defaultThresholdsControllerHelper.GetDefaultThresholds(customerId, request);

            return Ok(result);
        }
    }
}