using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Domain.Entities;
using CustomerService.DomainLogic.Services.Interfaces;
using CustomerService.Web.Api.Helpers.Interfaces;
using CustomerService.Web.Api.Models.Dtos;
using CustomerService.Web.Api.Models.Dtos.Site;


namespace CustomerService.Web.Api.Helpers.Implemetations
{
    /// <summary>
    /// SitesService.
    /// </summary>
    public class SitesControllerHelper : ISitesControllerHelper
    {
        private readonly ISiteService sitesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitesControllerHelper"/> class.
        /// </summary>
        /// <param name="sitesService">The customer site service.</param>
        public SitesControllerHelper(ISiteService sitesService)
        {
            this.sitesService = sitesService;
        }

        /// <summary>
        /// Creates the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Guid, SiteStatus>> CreateSite(
            int customerId,
            CreateSiteRequestDto model
        )
        {
            var site = Mapper.Map<CreateSiteRequestDto, Site>(model);
            site.CustomerId = customerId;

            var result = await sitesService.CreateSite(site);

            return result;
        }

        /// <summary>
        /// Updates the site asynchronous.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<SiteStatus> UpdateSite(
            int customerId, 
            Guid siteId,
            UpdateSiteRequestDto model
        )
        {
            var site = Mapper.Map<Site>(model);
            site.Id = siteId;
            site.CustomerId = customerId;

            return await sitesService.UpdateSite(site, model.IsArchived);
        }

        /// <summary>
        /// Gets all sites.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<SiteResponseDto>> GetSites(int customerId, SiteSearchDto request = null)
        {
            var result = await sitesService.GetSites(customerId, request);

            return Mapper.Map<PagedResultDto<SiteResponseDto>>(result);
        }

        /// <summary>
        /// Gets the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <returns></returns>
        public async Task<SiteResponseDto> GetSite(int customerId, Guid siteId)
        {
            var site = await sitesService.GetSiteById(customerId, siteId);

            return Mapper.Map<Site, SiteResponseDto>(site);
        }

        /// <summary>
        /// Gets the customer site dto asynchronous.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <returns></returns>
        public async Task<SiteResponseDto> GetSiteByName(int customerId, string siteName)
        {
            var site = await sitesService.GetSiteByName(siteName, customerId);

            return Mapper.Map<SiteResponseDto>(site);
        }

        /// <summary>
        /// Gets the site by customer identifier and site npi asynchronous.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteNpi">The site npi.</param>
        /// <returns></returns>
        public async Task<SiteResponseDto> GetSiteBySiteNpi(int customerId, string siteNpi)
        {
            var site = await sitesService.GetSiteByNpi(customerId, siteNpi);

            return Mapper.Map<SiteResponseDto>(site);
        }

        /// <summary>
        /// Gets the site by customer identifier and customer site identifier asynchronous.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerSiteId">The customer site identifier.</param>
        /// <returns></returns>
        public async Task<SiteResponseDto> GetSiteByCustomerSiteId(int customerId, string customerSiteId)
        {
            var site = await sitesService.GetSiteByCustomerSiteId(customerId, customerSiteId);

            return Mapper.Map<SiteResponseDto>(site);
        }

        /// <summary>
        /// Deletes the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <returns></returns>
        public Task<bool> DeleteSite(int customerId, Guid siteId)
        {
            return sitesService.DeleteSite(customerId, siteId);
        }
    }
}