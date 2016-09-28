using System;
using System.Runtime.Serialization;

namespace VitalsService.Web.Api.Exceptions
{
    /// <summary>
    /// Raise this exception when attribute is used in incorrect place or context.
    /// </summary>
    public class AttributeUsageException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeUsageException"/> class.
        /// </summary>
        public AttributeUsageException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeUsageException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AttributeUsageException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeUsageException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public AttributeUsageException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeUsageException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected AttributeUsageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}