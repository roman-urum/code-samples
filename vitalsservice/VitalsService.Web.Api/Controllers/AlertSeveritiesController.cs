using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Extensions;
using VitalsService.Web.Api.Filters;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.AlertSeverities;
using VitalsService.Web.Api.Models.Enums;

namespace VitalsService.Web.Api.Controllers
{
    /// <summary>
    /// AlertSeveritiesController.
    /// </summary>
    [TokenAuthorize]
    //[RoutePrefix(@"api/{customerId:regex(^[1-9]\d*)}/severities")]
    public class AlertSeveritiesController : ApiController
    {
        private readonly IAlertSeveritiesControllerHelper alertSeveritiesControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertSeveritiesController"/> class.
        /// </summary>
        /// <param name="alertSeveritiesControllerHelper">The alert severities controller helper.</param>
        public AlertSeveritiesController(IAlertSeveritiesControllerHelper alertSeveritiesControllerHelper)
        {
            this.alertSeveritiesControllerHelper = alertSeveritiesControllerHelper;
        }

        /// <summary>
        /// Creates the alert severity.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/severities/")]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        [InvalidateCacheOutput(nameof(GetAlertSeverities), typeof(AlertSeveritiesController), "customerId")]
        [InvalidateCacheOutput(nameof(GetAlertSeverity), typeof(AlertSeveritiesController), "customerId")]
        [InvalidateCacheOutput("GetThreshold", typeof(ThresholdsController), "customerId")]
        [InvalidateCacheOutput("GetThresholds", typeof(ThresholdsController), "customerId")]
        [InvalidateCacheOutput("GetDefaultThreshold", typeof(DefaultThresholdsController), "customerId")]
        [InvalidateCacheOutput("GetDefaultThresholds", typeof(DefaultThresholdsController), "customerId")]
        public async Task<IHttpActionResult> CreateAlertSeverity(
            int customerId,
            AlertSeverityRequestDto request
        )
        {
            var result = await alertSeveritiesControllerHelper.CreateAlertSeverity(customerId, request);

            return Created(
                new Uri(Request.RequestUri, result.ToString()),
                new PostResponseDto<Guid> { Id = result }
            );
        }

        /// <summary>
        /// Updates the alert severity.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="alertSeverityId">The alert severity identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/severities/{alertSeverityId:guid}")]
        [InvalidateCacheOutput(nameof(GetAlertSeverities), typeof(AlertSeveritiesController), "customerId")]
        [InvalidateCacheOutput(nameof(GetAlertSeverity), typeof(AlertSeveritiesController), "customerId", "alertSeverityId")]
        [InvalidateCacheOutput("GetThreshold", typeof(ThresholdsController), "customerId")]
        [InvalidateCacheOutput("GetThresholds", typeof(ThresholdsController), "customerId")]
        [InvalidateCacheOutput("GetDefaultThreshold", typeof(DefaultThresholdsController), "customerId")]
        [InvalidateCacheOutput("GetDefaultThresholds", typeof(DefaultThresholdsController), "customerId")]
        public async Task<IHttpActionResult> UpdateAlertSeverity(
            int customerId,
            Guid alertSeverityId,
            AlertSeverityRequestDto request
        )
        {
            var result = await alertSeveritiesControllerHelper.UpdateAlertSeverity(alertSeverityId, customerId, request);

            if (result.HasFlag(CreateUpdateDeleteAlertSeverityStatus.NotFound))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.GetConcatString()
                    }
                );
            }

            if (!result.HasFlag(CreateUpdateDeleteAlertSeverityStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.GetConcatString()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes the alert severity.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="alertSeverityId">The alert severity identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/severities/{alertSeverityId:guid}")]
        [InvalidateCacheOutput(nameof(GetAlertSeverities), typeof(AlertSeveritiesController), "customerId")]
        [InvalidateCacheOutput(nameof(GetAlertSeverity), typeof(AlertSeveritiesController), "customerId", "alertSeverityId")]
        [InvalidateCacheOutput("GetThreshold", typeof(ThresholdsController), "customerId")]
        [InvalidateCacheOutput("GetThresholds", typeof(ThresholdsController), "customerId")]
        [InvalidateCacheOutput("GetDefaultThreshold", typeof(DefaultThresholdsController), "customerId")]
        [InvalidateCacheOutput("GetDefaultThresholds", typeof(DefaultThresholdsController), "customerId")]
        public async Task<IHttpActionResult> DeleteAlertSeverity(int customerId, Guid alertSeverityId)
        {
            var operationResult = await alertSeveritiesControllerHelper.DeleteAlertSeverity(customerId, alertSeverityId);

            if (operationResult.HasFlag(CreateUpdateDeleteAlertSeverityStatus.NotFound))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.GetConcatString()
                    }
                );
            }

            if (!operationResult.HasFlag(CreateUpdateDeleteAlertSeverityStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.GetConcatString()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Gets the alert severity.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="alertSeverityId">The alert severity identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/severities/{alertSeverityId:guid}")]
        public async Task<IHttpActionResult> GetAlertSeverity(int customerId, Guid alertSeverityId)
        {
            var operationResult = await alertSeveritiesControllerHelper.GetAlertSeverity(customerId, alertSeverityId);

            if (operationResult.Status.HasFlag(GetAlertSeverityStatus.NotFound))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.Status.GetConcatString()
                    }
                );
            }

            if (!operationResult.Status.HasFlag(GetAlertSeverityStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.Status.GetConcatString()
                    }
                );
            }

            return this.Ok(operationResult.Content);
        }

        /// <summary>
        /// Gets the alert severities.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/severities/")]
        public async Task<IHttpActionResult> GetAlertSeverities(int customerId, [FromUri]AlertSeveritiesSearchDto searchRequest)
        {
            var alertSeverities = await alertSeveritiesControllerHelper.GetAlertSeverities(customerId, searchRequest);

            return this.Ok(alertSeverities);
        }
    }
}