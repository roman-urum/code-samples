using System;
using System.Runtime.Serialization;

namespace HealthLibrary.ContentStorage.Azure.Exceptions
{
    /// <summary>
    /// Raise this exception when provided url doesn't contain any content
    /// </summary>
    public class InvalidExternalUrlException : Exception
    {
        public InvalidExternalUrlException()
        {
        }

        public InvalidExternalUrlException(string message) : base(message)
        {
        }

        public InvalidExternalUrlException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidExternalUrlException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}