using System;

namespace Maestro.Web.Exceptions
{
    /// <summary>
    /// Raise this exception if static context used in inappropriate action.
    /// </summary>
    public class ContextUsageException : Exception
    {
        public ContextUsageException()
        {
        }

        public ContextUsageException(string message) : base(message)
        {
        }
    }
}