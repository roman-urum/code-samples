using System;
using System.Globalization;

namespace Maestro.Web.Exceptions
{
    /// <summary>
    /// Throw this exception for incorrect behaviour or incorect arguments.
    /// Use exception to show user that he was wrong.
    /// Will be handled by Application_Error.
    /// </summary>
    public class LogicException : Exception
    {
        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        /// <returns>The error message that explains the reason for 
        /// the exception, or an empty string("").</returns>
        public override string Message
        {
            get
            {
                return base.Message.ToString(CultureInfo.InvariantCulture);
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LogicException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public LogicException(String message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public LogicException(String message, Exception exception)
            : base(message, exception)
        {
        }
    }
}