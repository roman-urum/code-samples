using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Domain.Entities;
using CustomerService.DomainLogic.Services.Interfaces;
using CustomerService.Web.Api.Helpers.Interfaces;
using CustomerService.Web.Api.Models.Dtos;
using CustomerService.Web.Api.Models.Dtos.Customer;

namespace CustomerService.Web.Api.Helpers.Implemetations
{
    /// <summary>
    /// CustomersControllerHelper.
    /// </summary>
    public class CustomersControllerHelper : ICustomersControllerHelper
    {
        private readonly ICustomerService customerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersControllerHelper" /> class.
        /// </summary>
        /// <param name="customerService">The user customer service.</param>
        public CustomersControllerHelper(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        /// <summary>
        /// Creates the customer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<int, CustomerStatus>> CreateCustomer(
            CreateCustomerRequestDto model
        )
        {
            var customer = Mapper.Map<CreateCustomerRequestDto, Customer>(model);

            return await customerService.CreateCustomer(customer);
        }

        /// <summary>
        /// Updates the customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<CustomerStatus> UpdateCustomer(
            int customerId,
            UpdateCustomerRequestDto model
        )
        {
            var customer = Mapper.Map<UpdateCustomerRequestDto, Customer>(model);
            customer.Id = customerId;

            return await customerService.UpdateCustomer(customer, model.IsArchived);
        }

        /// <summary>
        /// Gets the customer model.
        /// </summary>
        /// <param name="customerId">The identifier.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        public async Task<BaseCustomerResponseDto> GetCustomer(int customerId, bool isBrief)
        {
            var customer = await customerService.GetCustomer(customerId);

            // Filtering archived customer's sites and organizations
            customer.Sites = customer.Sites.Where(s => !s.IsDeleted).ToList();
            customer.Organizations = customer.Organizations.Where(s => !s.IsDeleted).ToList();

            if (isBrief)
            {
                return Mapper.Map<Customer, BriefCustomerResponseDto>(customer);
            }

            return Mapper.Map<Customer, FullCustomerResponseDto>(customer);
        }

        /// <summary>
        /// Gets customers.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="isBrief"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<BaseCustomerResponseDto>> GetCustomers(CustomersSearchDto request, bool isBrief)
        {
            var pagedCustomers = await customerService.GetCustomers(request);

            // Filtering archived customer's sites and organizations
            if (request != null && !request.IncludeArchived)
            {
                foreach (var customer in pagedCustomers.Results)
                {
                    customer.Sites = customer.Sites.Where(s => !s.IsDeleted).ToList();
                    customer.Organizations = customer.Organizations.Where(s => !s.IsDeleted).ToList();
                }
            }

            var result = new PagedResultDto<BaseCustomerResponseDto>()
            {
                Total = pagedCustomers.Total,
                Results = isBrief ?
                    Mapper.Map<IList<Customer>, IList<BriefCustomerResponseDto>>(pagedCustomers.Results).Cast<BaseCustomerResponseDto>().ToList() :
                    Mapper.Map<IList<Customer>, IList<FullCustomerResponseDto>>(pagedCustomers.Results).Cast<BaseCustomerResponseDto>().ToList()
            };

            return result;
        }

        /// <summary>
        /// Gets customer by name.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        public async Task<BaseCustomerResponseDto> GetCustomerByName(int customerId, string name, bool isBrief)
        {
            var customer = await customerService.GetCustomerByName(name, customerId);

            // Filtering archived customer's sites and organizations
            customer.Sites = customer.Sites.Where(s => !s.IsDeleted).ToList();
            customer.Organizations = customer.Organizations.Where(s => !s.IsDeleted).ToList();

            if (isBrief)
            {
                return Mapper.Map<Customer, BriefCustomerResponseDto>(customer);
            }

            return Mapper.Map<Customer, FullCustomerResponseDto>(customer);
        }

        /// <summary>
        /// Gets the customer by subdomain.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="subdomain">The subdomain.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        public async Task<BaseCustomerResponseDto> GetCustomerBySubdomain(int customerId, string subdomain, bool isBrief)
        {
            var customer = await customerService.GetCustomerBySubdomain(subdomain, customerId);

            // Filtering archived customer's sites and organizations
            customer.Sites = customer.Sites.Where(s => !s.IsDeleted).ToList();
            customer.Organizations = customer.Organizations.Where(s => !s.IsDeleted).ToList();

            if (isBrief)
            {
                return Mapper.Map<Customer, BriefCustomerResponseDto>(customer);
            }

            return Mapper.Map<Customer, FullCustomerResponseDto>(customer);
        }

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteCustomer(int customerId)
        {
            return await customerService.DeleteCustomer(customerId);
        }
    }
}