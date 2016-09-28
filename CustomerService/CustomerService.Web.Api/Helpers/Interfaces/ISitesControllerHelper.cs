using System;
using System.Threading.Tasks;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Web.Api.Models.Dtos;
using CustomerService.Web.Api.Models.Dtos.Site;

namespace CustomerService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// ISitesService.
    /// </summary>
    public interface ISitesControllerHelper
    {
        /// <summary>
        /// Gets all sites.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResultDto<SiteResponseDto>> GetSites(int customerId, SiteSearchDto request = null);

        /// <summary>
        /// Gets the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <returns></returns>
        Task<SiteResponseDto> GetSite(int customerId, Guid siteId);

        /// <summary>
        /// Creates the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<OperationResultDto<Guid, SiteStatus>> CreateSite(int customerId, CreateSiteRequestDto model);

        /// <summary>
        /// Updates the site asynchronous.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<SiteStatus> UpdateSite(int customerId, Guid siteId, UpdateSiteRequestDto model);

        /// <summary>
        /// Gets the customer site dto asynchronous.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <returns></returns>
        Task<SiteResponseDto> GetSiteByName(int customerId, string siteName);

        /// <summary>
        /// Gets the site by customer identifier and site npi asynchronous.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteNpi">The site npi.</param>
        /// <returns></returns>
        Task<SiteResponseDto> GetSiteBySiteNpi(int customerId, string siteNpi);

        /// <summary>
        /// Gets the site by customer identifier and customer site identifier asynchronous.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerSiteId">The customer site identifier.</param>
        /// <returns></returns>
        Task<SiteResponseDto> GetSiteByCustomerSiteId(int customerId, string customerSiteId);

        /// <summary>
        /// Deletes the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteSite(int customerId, Guid siteId);
    }
}
