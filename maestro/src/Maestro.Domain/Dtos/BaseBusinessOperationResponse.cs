using System;

namespace Maestro.Domain.Dtos
{
    /// <summary>
    /// BaseBusinessOperationResponse.
    /// </summary>
    public abstract class BaseBusinessOperationResponse<TErrorState>
        where TErrorState : struct, IComparable, IFormattable, IConvertible
    {
        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsValid {
            get
            {
                return !Error.HasValue;
            }
        }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public TErrorState? Error { get; set; }
    }
}