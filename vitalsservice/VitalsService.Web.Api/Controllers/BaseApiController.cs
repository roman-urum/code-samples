using System.Net;
using System.Web.Http;
using VitalsService.Extensions;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.Enums;

namespace VitalsService.Web.Api.Controllers
{
    /// <summary>
    /// Provides additional functionality for API controllers
    /// to handle requests and generate responses.
    /// </summary>
    public abstract class BaseApiController : ApiController
    {
        /// <summary>
        /// Generates response with bad request status in custom format.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public new IHttpActionResult BadRequest(string errorMessage)
        {
            return Content(
                HttpStatusCode.BadRequest,
                new ErrorResponseDto()
                {
                    Error = ErrorCode.InvalidRequest,
                    Message = ErrorCode.InvalidRequest.Description(),
                    Details = errorMessage
                }
            );
        }

        /// <summary>
        /// Generates response in custom format for cases when requested data is not found.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public IHttpActionResult NotFound(string errorMessage)
        {
            return Content(
                HttpStatusCode.NotFound,
                new ErrorResponseDto()
                {
                    Error = ErrorCode.InvalidRequest,
                    Message = ErrorCode.InvalidRequest.Description(),
                    Details = errorMessage
                }
            );
        }
    }
}