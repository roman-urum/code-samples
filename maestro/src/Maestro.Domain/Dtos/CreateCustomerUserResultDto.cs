using System;
using Maestro.Domain.Enums;

namespace Maestro.Domain.Dtos
{
    /// <summary>
    /// CreateCustomerUserResultDto.
    /// </summary>
    public class CreateCustomerUserResultDto :
        BaseBusinessOperationResponse<CreateUpdateCustomerUserErrorState>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }
    }
}