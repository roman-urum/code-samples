using System.Threading.Tasks;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Web.Api.Models.Dtos;
using CustomerService.Web.Api.Models.Dtos.Customer;

namespace CustomerService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// CustomerService interface.
    /// </summary>
    public interface ICustomersControllerHelper
    {
        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        Task<BaseCustomerResponseDto> GetCustomer(int customerId, bool isBrief);

        /// <summary>
        /// Gets customers.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        Task<PagedResultDto<BaseCustomerResponseDto>> GetCustomers(CustomersSearchDto request, bool isBrief);

        /// <summary>
        /// Creates the customer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<OperationResultDto<int, CustomerStatus>> CreateCustomer(CreateCustomerRequestDto model);

        /// <summary>
        /// Updates the customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<CustomerStatus> UpdateCustomer(int customerId, UpdateCustomerRequestDto model);

        /// <summary>
        /// Gets customer by name.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        Task<BaseCustomerResponseDto> GetCustomerByName(int customerId, string name, bool isBrief);

        /// <summary>
        /// Gets the customer by subdomain.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="subdomain">The subdomain.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        Task<BaseCustomerResponseDto> GetCustomerBySubdomain(int customerId, string subdomain, bool isBrief);

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteCustomer(int customerId);
    }
}