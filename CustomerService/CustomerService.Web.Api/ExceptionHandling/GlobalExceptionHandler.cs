using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using CustomerService.Common.Extensions;
using CustomerService.Web.Api.Models.Dtos;
using CustomerService.Web.Api.Models.Dtos.Enums;
using NLog;

namespace CustomerService.Web.Api.ExceptionHandling
{
    /// <summary>
    /// GlobalExceptionHandler.
    /// </summary>
    public class GlobalExceptionHandler : ExceptionHandler
    {
        private readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// When overridden in a derived class, handles the exception asynchronously.
        /// </summary>
        /// <param name="context">The exception handler context.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous exception handling operation.
        /// </returns>
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            Handle(context);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// When overridden in a derived class, handles the exception synchronously.
        /// </summary>
        /// <param name="context">The exception handler context.</param>
        public override void Handle(ExceptionHandlerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            log.Error(context.Exception, "Unhandled exception occured");

#if !DEBUG
            HttpRequestMessage request = context.ExceptionContext.Request;

            if (request == null)
            {
                throw new ArgumentException("Invalid argument provided.", "context");
            }

            context.Result = new ResponseMessageResult(
                request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InternalServerError,
                        Message = ErrorCode.InternalServerError.Description()
                    }));
#endif
        }
    }
}