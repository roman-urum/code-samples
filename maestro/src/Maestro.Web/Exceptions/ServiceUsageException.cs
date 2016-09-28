using System;

namespace Maestro.Web.Exceptions
{
    /// <summary>
    /// Raise this exception when service/manager used in incorrect place.
    /// </summary>
    public class ServiceUsageException : Exception
    {
        public ServiceUsageException()
        {
        }

        public ServiceUsageException(string message)
            : base(message)
        {
        }
    }
}