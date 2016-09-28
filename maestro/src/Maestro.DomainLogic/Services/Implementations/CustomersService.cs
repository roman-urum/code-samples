using System;
using System.Linq.Expressions;
using Maestro.DataAccess.EF.DataAccess;
using Maestro.Domain.DbEntities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.CustomerService;
using Maestro.DomainLogic.Services.Interfaces;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// CustomersService.
    /// </summary>
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersDataProvider customersDataProvider;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<CustomerUserRole> customerUserRolesRepository;
        private readonly IRepository<CustomerUserRoleToPermissionMapping> customerUserRolesPersmissionsRepository;
        private readonly IUsersService usersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersService"/> class.
        /// </summary>
        /// <param name="usersService">The users service.</param>
        /// <param name="customersDataProvider">The customers data provider.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public CustomersService(
            IUsersService usersService, 
            ICustomersDataProvider customersDataProvider, 
            IUnitOfWork unitOfWork
        )
        {
            this.customersDataProvider = customersDataProvider;
            this.unitOfWork = unitOfWork;
            this.customerUserRolesRepository = this.unitOfWork.CreateGenericRepository<CustomerUserRole>();
            this.customerUserRolesPersmissionsRepository = this.unitOfWork.CreateGenericRepository<CustomerUserRoleToPermissionMapping>();
            this.usersService = usersService;
        }

        /// <summary>
        /// Creates the customer.
        /// </summary>
        /// <param name="createCustomerData">The create customer data.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<PostResponseDto<int>> CreateCustomer(CreateCustomerRequestDto createCustomerData, string bearerToken)
        {
            var result = await customersDataProvider.CreateCustomer(createCustomerData, bearerToken);

            await SetCustomerUserRolesFromDefaultRoles(result.Id);

            return result;
        }

        /// <summary>
        /// Returns list of all existed customers.
        /// </summary>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<IList<CustomerResponseDto>> GetCustomers(string bearerToken)
        {
            return await customersDataProvider.GetCustomers(bearerToken);
        }

        /// <summary>
        /// Gets the customers asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<IList<CustomerResponseDto>> GetCustomers(Guid userId, string bearerToken)
        {
            var user = await usersService.GetUser(userId) as CustomerUser;

            if (user != null)
            {
                return new List<CustomerResponseDto>() { await GetCustomer(user.CustomerId, bearerToken) };
            }

            return new List<CustomerResponseDto>();
        }

        /// <summary>
        /// Return customer by customer id
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<CustomerResponseDto> GetCustomer(int customerId, string bearerToken)
        {
            return await customersDataProvider.GetCustomer(customerId, bearerToken);
        }

        /// <summary>
        /// Return customer subdomain.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="subdomain">Subdomain of customer site.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public CustomerResponseDto GetCustomer(int customerId, string subdomain, string bearerToken)
        {
            return customersDataProvider.GetCustomerBySubdomain(customerId, subdomain, bearerToken);
        }

        /// <summary>
        /// Sends request to customer service to update customer data
        /// by specified id.
        /// </summary>
        /// <param name="inCustomer">The in customer.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task UpdateCustomer(UpdateCustomerRequestDto inCustomer, string bearerToken)
        {
            await customersDataProvider.UpdateCustomer(inCustomer, bearerToken);
        }

        /// <summary>
        /// Uploads customer logo using customer service.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns>
        /// Path to logo
        /// </returns>
        public async Task<string> UploadLogo(FileDto file, string bearerToken)
        {
            return await customersDataProvider.UploadLogo(file, bearerToken);
        }

        /// <summary>
        /// Gets customer's roles
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<IList<CustomerUserRole>> GetCustomerUserRoles(int customerId)
        {
            var includes = new List<Expression<Func<CustomerUserRole, object>>> { e => e.Permissions };

            return await customerUserRolesRepository.FindAsync(r => r.CustomerId == customerId, null, includes);
        }

        /// <summary>
        /// Creates the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="newSite">The new site.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task<PostResponseDto<Guid>> CreateSite(int customerId, SiteRequestDto newSite, string bearerToken)
        {
            return customersDataProvider.CreateSite(customerId, newSite, bearerToken);
        }

        /// <summary>
        /// Updates the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="site">The site.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task UpdateSite(int customerId, Guid siteId, SiteRequestDto site, string bearerToken)
        {
            return customersDataProvider.UpdateSite(customerId, siteId, site, bearerToken);
        }

        /// <summary>
        /// Deletes the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task DeleteSite(int customerId, Guid siteId, string bearerToken)
        {
            return customersDataProvider.DeleteSite(customerId, siteId, bearerToken);
        }

        /// <summary>
        /// Creates the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organization">The organization.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task<PostResponseDto<Guid>> CreateOrganization(int customerId, OrganizationRequestDto organization, string bearerToken)
        {
            return customersDataProvider.CreateOrganization(customerId, organization, bearerToken);
        }

        /// <summary>
        /// Updates the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="organization">The organization.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task UpdateOrganization(int customerId, Guid organizationId, OrganizationRequestDto organization, string bearerToken)
        {
            return customersDataProvider.UpdateOrganization(customerId, organizationId, organization, bearerToken);
        }

        /// <summary>
        /// Deletes the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task DeleteOrganization(int customerId, Guid organizationId, string bearerToken)
        {
            return customersDataProvider.DeleteOrganization(customerId, organizationId, bearerToken);
        }

        /// <summary>
        /// Gets the sites.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task<IList<SiteResponseDto>> GetSites(int customerId, SiteSearchDto request, string bearerToken)
        {
            return customersDataProvider.GetSites(customerId, request, bearerToken);
        }

        #region Private methods

        /// <summary>
        /// Copy default roles to customer.
        /// Should be used during creation of new customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private async Task SetCustomerUserRolesFromDefaultRoles(int customerId)
        {
            var includes = new List<Expression<Func<CustomerUserRole, object>>> {role => role.Permissions};
            var defaultRoles = await customerUserRolesRepository
                .FindAsync(
                    r => !r.CustomerId.HasValue,
                    null,
                    includes
                );

            foreach (var defaultRole in defaultRoles)
            {
                var customerUserRole = new CustomerUserRole {CustomerId = customerId, Name = defaultRole.Name};

                customerUserRolesRepository.Insert(customerUserRole);

                foreach (var permission in defaultRole.Permissions)
                {
                    customerUserRolesPersmissionsRepository.Insert(
                        new CustomerUserRoleToPermissionMapping()
                        {
                            CustomerUserRole = customerUserRole,
                            PermissionCode = permission.PermissionCode
                        });
                }
            }

            await unitOfWork.SaveAsync();
        }

        #endregion
    }
}