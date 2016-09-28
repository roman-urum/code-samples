using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.CustomerService
{
    /// <summary>
    /// UpdateCustomerRequestDto.
    /// </summary>
    [JsonObject]
    public class UpdateCustomerRequestDto : CreateCustomerRequestDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public int Id { get; set; }
    }
}