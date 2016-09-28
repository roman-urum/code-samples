using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using HealthLibrary.Common.Extensions;
using HealthLibrary.ContentStorage.Azure.Exceptions;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Enums;

namespace HealthLibrary.Web.Api.Filters.Exceptions
{
    /// <summary>
    /// InvalidContentFilterAttribute.
    /// </summary>
    /// <seealso cref="System.Web.Http.Filters.ExceptionFilterAttribute" />
    public class InvalidContentFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Called when [exception].
        /// </summary>
        /// <param name="context">The context.</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is InvalidExternalUrlException || context.Exception is InvalidFileContentException)
            {
                context.Response = context.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = context.Exception.Message
                    }
                );
            }
        }
    }
}