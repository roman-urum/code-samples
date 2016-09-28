using System;
using System.Runtime.Serialization;

namespace HealthLibrary.ContentStorage.Azure.Exceptions
{
    /// <summary>
    /// Raise this exception when file contains incorrect content
    /// or if content don't matches to expected.
    /// </summary>
    public class InvalidFileContentException : Exception
    {
        public InvalidFileContentException()
        {
        }

        public InvalidFileContentException(string message) : base(message)
        {
        }

        public InvalidFileContentException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidFileContentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
