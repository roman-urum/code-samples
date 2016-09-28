using NLog;
using System.Web.Http.ExceptionHandling;

public class NLogExceptionLogger : ExceptionLogger
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public override void Log(ExceptionLoggerContext context)
    {
        logger.Error(context.Exception, "Unhandled exception");
    }
}
