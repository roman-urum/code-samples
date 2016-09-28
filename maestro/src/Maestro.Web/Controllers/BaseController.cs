using System.Net;
using System.Web.Mvc;
using Maestro.Common.Extensions;
using Maestro.Web.Models.Api.Dtos.Enums;
using Maestro.Web.Models.Api.Dtos.RequestsResponses;
using Maestro.Web.Security;

namespace Maestro.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public virtual new IMaestroPrincipal User
        {
            get { return base.User as IMaestroPrincipal; }
        }

        /// <summary>
        /// Returns description of error as json in response with StatusCode = 400.
        /// </summary>
        /// <param name="errorDetails"></param>
        /// <returns></returns>
        public ActionResult BadRequest(string errorDetails)
        {
            Response.StatusCode = (int) HttpStatusCode.BadRequest;

            return Json(new ErrorResponseDto()
            {
                Error = ErrorCode.InvalidRequest,
                Message = ErrorCode.InvalidRequest.Description(),
                Details = errorDetails
            });
        }
    }
}