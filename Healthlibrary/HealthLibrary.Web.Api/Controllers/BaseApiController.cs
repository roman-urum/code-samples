using System.Net;
using System.Web.Http;
using HealthLibrary.Common.Extensions;
using HealthLibrary.DomainLogic.Services.Results;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Enums;

namespace HealthLibrary.Web.Api.Controllers
{
    /// <summary>
    /// Provides common functionality for API controllers
    /// </summary>
    public abstract class BaseApiController : ApiController
    {
        /// <summary>
        /// Generates action result using response from service.
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected IHttpActionResult HandleServiceActionResult(ServiceActionResultStatus status)
        {
            if (status == ServiceActionResultStatus.DataNotFound)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Handles bad requests with custom response format.
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        protected new IHttpActionResult BadRequest(string details)
        {
            return Content(
                HttpStatusCode.BadRequest,
                new ErrorResponseDto()
                {
                    Error = ErrorCode.InvalidRequest,
                    Message = ErrorCode.InvalidRequest.Description(),
                    Details = details
                });
        }

        /// <summary>
        /// Handles cases when data not found with custom response format.
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        protected IHttpActionResult NotFound(string details)
        {
            return Content(
                HttpStatusCode.NotFound,
                new ErrorResponseDto()
                {
                    Error = ErrorCode.InvalidRequest,
                    Message = ErrorCode.InvalidRequest.Description(),
                    Details = details
                });
        }
    }
}