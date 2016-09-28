using System;

namespace VitalsService.Domain.Dtos
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

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResultDto{TContent, TStatus}"/> class.
        /// </summary>
        public OperationResultDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResultDto{TContent, TStatus}"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        public OperationResultDto(TStatus status)
        {
            this.Status = status;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResultDto{TContent, TStatus}"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="content">The content.</param>
        public OperationResultDto(TStatus status, TContent content)
            : this(status)
        {
            this.Content = content;
        }
    }
}