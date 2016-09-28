using Maestro.Domain.DbEntities;
using Maestro.Domain.Enums;

namespace Maestro.Domain.Dtos
{
    /// <summary>
    /// UpdateCustomerUserResultDto.
    /// </summary>
    public class UpdateCustomerUserResultDto :
        BaseBusinessOperationResponse<CreateUpdateCustomerUserErrorState>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public CustomerUser User { get; set; }
    }
}