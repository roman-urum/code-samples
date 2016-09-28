namespace CustomerService.Web.Api.Models.Dtos.Customer
{
    /// <summary>
    /// UpdateCustomerRequestDto.
    /// </summary>
    public class UpdateCustomerRequestDto : CreateCustomerRequestDto
    {
        /// <summary>
        /// Gets or sets the is archived.
        /// </summary>
        /// <value>
        /// The is archived.
        /// </value>
        public bool? IsArchived { get; set; }
    }
}