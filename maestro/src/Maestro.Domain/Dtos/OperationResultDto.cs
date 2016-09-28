using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maestro.Domain.Dtos
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
    }
}
