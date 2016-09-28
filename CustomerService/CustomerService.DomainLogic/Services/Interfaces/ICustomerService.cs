using System.Threading.Tasks;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Domain.Entities;

namespace CustomerService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// ICustomerService.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="includeSoftDeleted">if set to <c>true</c> [include soft deleted].</param>
        /// <returns></returns>
        Task<Customer> GetCustomer(int customerId, bool includeSoftDeleted = false);

        /// <summary>
        /// Creates the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns></returns>
        Task<OperationResultDto<int, CustomerStatus>> CreateCustomer(Customer customer);

        /// <summary>
        /// Gets customers.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResult<Customer>> GetCustomers(CustomersSearchDto request);

        /// <summary>
        /// Updates the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="isDeleteRequested">The is delete requested.</param>
        /// <returns></returns>
        Task<CustomerStatus> UpdateCustomer(Customer customer, bool? isDeleteRequested);

        /// <summary>
        /// Gets the name of the customer by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Task<Customer> GetCustomerByName(string name, int? customerId = null);

        /// <summary>
        /// Gets the customer by subdomain.
        /// </summary>
        /// <param name="subdomain">The subdomain.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Task<Customer> GetCustomerBySubdomain(string subdomain, int? customerId = null);

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteCustomer(int customerId);
    }
}
