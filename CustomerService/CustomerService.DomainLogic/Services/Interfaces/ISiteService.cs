using System;
using System.Threading.Tasks;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Domain.Entities;

namespace CustomerService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// ICustomerSiteService.
    /// </summary>
    public interface ISiteService
    {
        /// <summary>
        /// Gets all sites.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedResult<Site>> GetSites(int customerId, SiteSearchDto request = null);

        /// <summary>
        /// Gets the site by identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <returns></returns>
        Task<Site> GetSiteById(int customerId, Guid siteId);

        /// <summary>
        /// Creates the site.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <returns></returns>
        Task<OperationResultDto<Guid, SiteStatus>> CreateSite(Site site);

        /// <summary>
        /// Updates the site asynchronous.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="isDeleteRequested">The is delete requested.</param>
        /// <returns></returns>
        Task<SiteStatus> UpdateSite(Site site, bool? isDeleteRequested);

        /// <summary>
        /// Gets the name of the customer site by.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <returns></returns>
        Task<Site> GetCustomerSiteByName(int customerId, string siteName);

        /// <summary>
        /// Gets the name of the site by.
        /// </summary>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Task<Site> GetSiteByName(string siteName, int? customerId = null);

        /// <summary>
        /// Gets the site by customer identifier and site NPI.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteNpi">The site NPI.</param>
        /// <returns></returns>
        Task<Site> GetSiteByNpi(int customerId, string siteNpi);

        /// <summary>
        /// Gets the site by customer identifier and customer site identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerSiteId">The customer site identifier.</param>
        /// <returns></returns>
        Task<Site> GetSiteByCustomerSiteId(int customerId, string customerSiteId);

        /// <summary>
        /// Deletes the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteSite(int customerId, Guid siteId);
    }
}
