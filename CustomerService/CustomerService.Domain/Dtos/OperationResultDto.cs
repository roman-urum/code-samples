using System;

namespace CustomerService.Domain.Dtos
{
    /// <summary>
    /// Base class for CRUD operations in Business layer.
    /// </summary>
    public class OperationResultDto<TContent, TStatus>
        where TStatus : struct, IComparable, IConvertible, IFormattable
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public TContent Content { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public TStatus Status { get; set; }
    }
}