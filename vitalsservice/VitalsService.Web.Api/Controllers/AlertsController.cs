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
using VitalsService.Web.Api.Models.Alerts;
using VitalsService.Web.Api.Models.Enums;

namespace VitalsService.Web.Api.Controllers
{
    /// <summary>
    /// AlertsController.
    /// </summary>
    [TokenAuthorize]
    [RoutePrefix(@"api/{customerId:regex(^[1-9]\d*)}/alerts")]
    public class AlertsController : ApiController
    {
        private readonly IAlertsControllerHelper alertsControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertsController"/> class.
        /// </summary>
        /// <param name="alertsControllerHelper">The alerts controller helper.</param>
        public AlertsController(IAlertsControllerHelper alertsControllerHelper)
        {
            this.alertsControllerHelper = alertsControllerHelper;
        }

        /// <summary>
        /// Creates the alert.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        public async Task<IHttpActionResult> CreateAlert(
            int customerId,
            AlertRequestDto request
        )
        {
            var result = await alertsControllerHelper.CreateAlert(customerId, request);

            if (result.Status.HasFlag(CreateUpdateAlertStatus.AlertSeverityWithSuchIdDoesNotExist))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.Status.GetConcatString()
                    }
                );
            }

            if (!result.Status.HasFlag(CreateUpdateAlertStatus.Success))
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
        /// Acknowledges the alerts.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("acknowledge")]
        public async Task<IHttpActionResult> AcknowledgeAlerts(
            int customerId,
            AcknowledgeAlertsRequestDto request
        )
        {
            var result = await alertsControllerHelper.AcknowledgeAlerts(customerId, request);

            if (!result.HasFlag(CreateUpdateAlertStatus.Success))
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
        /// Gets the alerts.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("getalerts")]
        [ResponseType(typeof(PagedResultDto<PatientAlertsDto>))]
        [SwaggerResponse(HttpStatusCode.OK, "Response with alerts.", typeof(PagedResultDto<PatientAlertsDto>))]
        public IHttpActionResult GetAlerts(
            int customerId,
            AlertsSearchDto request
        )
        {
            var result = alertsControllerHelper.GetAlerts(customerId, request);

            return Ok(result);
        }
    }
}