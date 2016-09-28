using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CustomerService.Common.Helpers;
using CustomerService.DataAccess;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Domain.Entities;
using CustomerService.DomainLogic.Services.Interfaces;

namespace CustomerService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// CustomerService.
    /// </summary>
    public class CustomersService : ICustomerService
    {
        private readonly int MaestroAdminsCustomerId = 1;

        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Customer> customerRepository;

        private readonly List<Expression<Func<Customer, object>>> CustomerIncludes = 
            new List<Expression<Func<Customer, object>>>
            {
                e => e.Sites.Select(s => s.CategoriesOfCare),
                e => e.CategoriesOfCare,
                e => e.Organizations
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public CustomersService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.customerRepository = this.unitOfWork.CreateGenericRepository<Customer>();
        }

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="includeSoftDeleted">if set to <c>true</c> [include soft deleted].</param>
        /// <returns></returns>
        public async Task<Customer> GetCustomer(int customerId, bool includeSoftDeleted = false)
        {
            if (includeSoftDeleted)
            {
                return (await customerRepository
                    .FindAsync(
                        c => c.Id == customerId,
                        o => o.OrderBy(e => e.Id),
                        CustomerIncludes
                    )).SingleOrDefault();
            }

            return (await customerRepository
                .FindAsync(
                    c => c.Id == customerId && !c.IsDeleted,
                    o => o.OrderBy(e => e.Id),
                    CustomerIncludes
                )).SingleOrDefault();
        }

        /// <summary>
        /// Gets the name of the customer by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public async Task<Customer> GetCustomerByName(string name, int? customerId = null)
        {
            if (customerId.HasValue && customerId.Value != MaestroAdminsCustomerId)
            {
                return (await customerRepository
                    .FindAsync(
                        c => c.Name.ToLower() == name.ToLower() &&
                            c.Id == customerId &&
                            !c.IsDeleted,
                        o => o.OrderBy(c => c.Id),
                        CustomerIncludes
                    ))
                    .FirstOrDefault();
            }

            return (await customerRepository
                .FindAsync(
                    c => c.Name.ToLower() == name.ToLower() &&
                        !c.IsDeleted,
                    o => o.OrderBy(c => c.Id),
                    CustomerIncludes
                ))
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets the customer by subdomain.
        /// </summary>
        /// <param name="subdomain">The subdomain.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public async Task<Customer> GetCustomerBySubdomain(string subdomain, int? customerId = null)
        {
            if (customerId.HasValue && customerId.Value != MaestroAdminsCustomerId)
            {
                return (await customerRepository
                    .FindAsync(
                        c => c.Subdomain.ToLower() == subdomain.ToLower() &&
                            c.Id == customerId &&
                            !c.IsDeleted,
                        o => o.OrderBy(c => c.Id),
                        CustomerIncludes
                    ))
                    .FirstOrDefault();
            }

            return (await customerRepository
                .FindAsync(
                    c => c.Subdomain.ToLower() == subdomain.ToLower() &&
                        !c.IsDeleted,
                    o => o.OrderBy(c => c.Id),
                    CustomerIncludes
                ))
                .FirstOrDefault();
        }

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteCustomer(int customerId)
        {
            var existingCustomer = await GetCustomer(customerId);

            if (existingCustomer != null)
            {
                existingCustomer.IsDeleted = true;

                foreach (var site in existingCustomer.Sites)
                {
                    site.IsDeleted = true;
                }

                foreach (var organization in existingCustomer.Organizations)
                {
                    organization.IsDeleted = true;
                }

                await unitOfWork.SaveAsync();

                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }

        /// <summary>
        /// Creates the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<int, CustomerStatus>> CreateCustomer(Customer customer)
        {
            var validationResult = await ValidateCustomer(customer);

            if ((validationResult & CustomerStatus.Success) == CustomerStatus.Success)
            {
                customerRepository.Insert(customer);

                await unitOfWork.SaveAsync();
            }

            return new OperationResultDto<int, CustomerStatus>()
            {
                Content = customer.Id,
                Status = validationResult
            };
        }

        /// <summary>
        /// Gets customers.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResult<Customer>> GetCustomers(CustomersSearchDto request)
        {
            Expression<Func<Customer, bool>> expression = c => true;

            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.Q))
                {
                    var terms = request.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(c => c.Name.Contains(term) || c.Subdomain.Contains(term));
                    }
                }

                if (!request.IncludeArchived)
                {
                    expression = expression.And(c => !c.IsDeleted);
                }
            }

            return await customerRepository
                .FindPagedAsync(
                    expression,
                    o => o.OrderBy(e => e.Name),
                    CustomerIncludes,
                    request != null ? request.Skip : (int?)null,
                    request != null ? request.Take : (int?)null
                );
        }

        /// <summary>
        /// Updates the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="isDeleteRequested">The is delete requested.</param>
        /// <returns></returns>
        public async Task<CustomerStatus> UpdateCustomer(Customer customer, bool? isDeleteRequested)
        {
            var existingCustomer = await GetCustomer(customer.Id, true);

            if (existingCustomer == null)
            {
                return CustomerStatus.NotFound;
            }

            var validationResult = await ValidateCustomer(customer);

            if ((validationResult & CustomerStatus.Success) != CustomerStatus.Success)
            {
                return validationResult;
            }

            existingCustomer.Name = customer.Name;
            existingCustomer.Subdomain = customer.Subdomain;
            existingCustomer.IddleSessionTimeout = customer.IddleSessionTimeout;
            existingCustomer.LogoPath = customer.LogoPath;
            existingCustomer.PasswordExpirationDays = customer.PasswordExpirationDays;

            if (isDeleteRequested.HasValue)
            {
                existingCustomer.IsDeleted = isDeleteRequested.Value;
            }

            customerRepository.Update(existingCustomer);

            await unitOfWork.SaveAsync();

            return CustomerStatus.Success;
        }

        /// <summary>
        /// Validates the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns></returns>
        private async Task<CustomerStatus> ValidateCustomer(Customer customer)
        {
            var validationResult = CustomerStatus.Success;

            var сonflictedCustomers = await customerRepository.FindAsync(c => (c.Name.ToLower() == customer.Name.ToLower() || c.Subdomain.ToLower() == customer.Subdomain.ToLower() && !c.IsDeleted));

            if (сonflictedCustomers != null && сonflictedCustomers.Any())
            {
                if (!customer.IsNew)
                {
                    var currentCustomer = сonflictedCustomers.SingleOrDefault(c => c.Id == customer.Id);

                    сonflictedCustomers.Remove(currentCustomer);
                }

                if (сonflictedCustomers.Any(c => c.Name.ToLower() == customer.Name.ToLower()))
                {
                    validationResult = CustomerStatus.NameConflict;
                }

                if (сonflictedCustomers.Any(c => c.Subdomain.ToLower() == customer.Subdomain.ToLower()))
                {
                    if (validationResult.HasFlag(CustomerStatus.Success))
                    {
                        validationResult = CustomerStatus.SubdomainConflict;
                    }
                    else
                    {
                        validationResult |= CustomerStatus.SubdomainConflict;
                    }
                }
            }

            return validationResult;
        }
    }
}