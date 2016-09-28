using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.CustomerService;

namespace Maestro.DataAccess.Api.DataProviders.Interfaces
{
    /// <summary>
    /// ICustomersDataProvider.
    /// </summary>
    public interface ICustomersDataProvider
    {
        /// <summary>
        /// Creates the customer.
        /// </summary>
        /// <param name="createCustomerData">The create customer data.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<PostResponseDto<int>> CreateCustomer(CreateCustomerRequestDto createCustomerData, string bearerToken);

        /// <summary>
        /// Returns list of all existed customers.
        /// </summary>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<IList<CustomerResponseDto>> GetCustomers(string bearerToken);

        /// <summary>
        /// Return customer by customer id
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<CustomerResponseDto> GetCustomer(int customerId, string bearerToken);

        /// <summary>
        /// Return customer by customer subdomain.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="subdomain">Subdomain of customer site.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        CustomerResponseDto GetCustomerBySubdomain(int customerId, string subdomain, string bearerToken);

        /// <summary>
        /// Updates customer data in customers service by customer id.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task UpdateCustomer(UpdateCustomerRequestDto customer, string bearerToken);

        /// <summary>
        /// Uploads customer logo using customer service.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns>
        /// Path to logo
        /// </returns>
        Task<string> UploadLogo(FileDto file, string bearerToken);

        /// <summary>
        /// Creates the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="newSite">The new site.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateSite(int customerId, SiteRequestDto newSite, string bearerToken);

        /// <summary>
        /// Updates the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="site">The site.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task UpdateSite(int customerId, Guid siteId, SiteRequestDto site, string bearerToken);

        /// <summary>
        /// Deletes the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task DeleteSite(int customerId, Guid siteId, string bearerToken);

        /// <summary>
        /// Creates the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organization">The organization.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateOrganization(int customerId, OrganizationRequestDto organization, string bearerToken);

        /// <summary>
        /// Updates the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="organization">The organization.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task UpdateOrganization(int customerId, Guid organizationId, OrganizationRequestDto organization, string bearerToken);

        /// <summary>
        /// Deletes the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task DeleteOrganization(int customerId, Guid organizationId, string bearerToken);

        /// <summary>
        /// Gets the sites.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<IList<SiteResponseDto>> GetSites(int customerId, SiteSearchDto request, string bearerToken);
    }
}