using System;

namespace DeviceService.Domain.Dtos
{
    /// <summary>
    /// Base class for CRUD operations in Business layer.
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    /// <typeparam name="TStatus"></typeparam>
    public class OperationResultDto<TContent, TStatus> where TStatus : struct, IComparable, IConvertible, IFormattable
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

        public OperationResultDto()
        {
        }

        public OperationResultDto(TStatus status)
        {
            this.Status = status;
        }

        public OperationResultDto(TStatus status, TContent content)
            : this(status)
        {
            this.Content = content;
        }

        /// <summary>
        /// Creates clone of current entity with the same status.
        /// </summary>
        /// <typeparam name="TClone"></typeparam>
        /// <returns></returns>
        public OperationResultDto<TClone, TStatus> Clone<TClone>()
        {
            return new OperationResultDto<TClone, TStatus>(this.Status);
        }
    }
}
