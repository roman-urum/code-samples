using System;

namespace HealthLibrary.Web.Api.Exceptions
{
    /// <summary>
    /// Raise this exception if static context used in inappropriate action.
    /// </summary>
    public class ContextUsageException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContextUsageException"/> class.
        /// </summary>
        public ContextUsageException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextUsageException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ContextUsageException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextUsageException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ContextUsageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}