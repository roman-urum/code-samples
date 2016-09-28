using System;
using System.Collections.Generic;
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
    /// ThresholdsController.
    /// </summary>
    [TokenAuthorize]
    [RoutePrefix(@"api/{customerId:regex(^[1-9]\d*)}/thresholds/{patientId:guid}")]
    public class ThresholdsController : ApiController
    {
        private readonly IThresholdsControllerHelper thresholdsControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThresholdsController" /> class.
        /// </summary>
        /// <param name="thresholdsControllerHelper">The thresholds controller helper.</param>
        public ThresholdsController(IThresholdsControllerHelper thresholdsControllerHelper)
        {
            this.thresholdsControllerHelper = thresholdsControllerHelper;
        }

        /// <summary>
        /// Creates the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        [InvalidateCacheOutput(nameof(GetThresholds), typeof(ThresholdsController), "customerId", "patientId")]
        [InvalidateCacheOutput(nameof(GetThreshold), typeof(ThresholdsController), "customerId", "patientId")]
        public async Task<IHttpActionResult> CreateThreshold(
            int customerId,
            Guid patientId,
            ThresholdRequestDto request
        )
        {
            var result = await thresholdsControllerHelper.CreateThreshold(customerId, patientId, request);

            if (!result.Status.HasFlag(CreateUpdateThresholdStatus.Success))
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
        /// Updates the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdId">The threshold identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{thresholdId:guid}")]
        [SwaggerResponse(HttpStatusCode.OK, "Threshold was updated.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Threshold does not exist.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in the request.")]
        [InvalidateCacheOutput(nameof(GetThresholds), typeof(ThresholdsController), "customerId", "patientId")]
        [InvalidateCacheOutput(nameof(GetThreshold), typeof(ThresholdsController), "customerId", "patientId", "thresholdId")]
        [InvalidateCacheOutput("GetVital", typeof(VitalsController), "customerId", "patientId")]
        [InvalidateCacheOutput("GetVitals", typeof(VitalsController), "customerId", "patientId")]
        public async Task<IHttpActionResult> UpdateThreshold(
            int customerId,
            Guid patientId,
            Guid thresholdId,
            ThresholdRequestDto request
        )
        {
            var status = await thresholdsControllerHelper.UpdateThreshold(customerId, patientId, thresholdId, request);

            if (!status.HasFlag(CreateUpdateThresholdStatus.Success))
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
        /// Deletes the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdId">The threshold identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{thresholdId:guid}")]
        [SwaggerResponse(HttpStatusCode.OK, "Threshold was deleted.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Threshold with such identifier does not exist.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in the request.")]
        [InvalidateCacheOutput(nameof(GetThresholds), typeof(ThresholdsController), "customerId", "patientId")]
        [InvalidateCacheOutput(nameof(GetThreshold), typeof(ThresholdsController), "customerId", "patientId", "thresholdId")]
        [InvalidateCacheOutput("GetVital", typeof(VitalsController), "customerId", "patientId")]
        [InvalidateCacheOutput("GetVitals", typeof(VitalsController), "customerId", "patientId")]
        public async Task<IHttpActionResult> DeleteThreshold(
            int customerId,
            Guid patientId,
            Guid thresholdId
        )
        {
            var status = await thresholdsControllerHelper.DeleteThreshold(customerId, patientId, thresholdId);

            if (status == GetDeleteThresholdStatus.NotFound)
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
        /// Gets the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdId">The threshold identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [CertificateAuthorize]
        [Route("{thresholdId:guid}")]
        [ResponseType(typeof(PatientThresholdDto))]
        [SwaggerResponse(HttpStatusCode.OK, "Existing threshold.", typeof(PatientThresholdDto))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Threshold does not exist.")]
        public async Task<IHttpActionResult> GetThreshold(
            int customerId,
            Guid patientId,
            Guid thresholdId
        )
        {
            var result = await thresholdsControllerHelper.GetThreshold(customerId, patientId, thresholdId);

            if (result.Status == GetDeleteThresholdStatus.NotFound)
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
        /// Gets the thresholds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [CertificateAuthorize]
        [Route("")]
        [ResponseType(typeof(PagedResultDto<BaseThresholdDto>))]
        [SwaggerResponse(HttpStatusCode.OK, "Response with thresholds.", typeof(PagedResultDto<BaseThresholdDto>))]
        public async Task<IHttpActionResult> GetThresholds(
            int customerId,
            Guid patientId,
            [FromUri]ThresholdsSearchDto request
        )
        {
            var result = await thresholdsControllerHelper.GetThresholds(customerId, patientId, request);

            return Ok(result);
        }
    }
}