using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using NLog;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.ExceptionHandling
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
        }
    }
}