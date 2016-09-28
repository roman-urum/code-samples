using System;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Web.Areas.Customer.Models.Settings.Sites;

namespace Maestro.Web.Areas.Customer.Managers.Interfaces
{
    /// <summary>
    /// ISettingsControllerManager.SitesAndOrgs
    /// </summary>
    public partial interface ISettingsControllerManager
    {
        /// <summary>
        /// Creates new site for current customer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateCustomerSite(CreateUpdateSiteViewModel model);

        /// <summary>
        /// Updates customer site data.
        /// </summary>
        /// <returns></returns>
        Task UpdateCustomerSite(Guid siteId, CreateUpdateSiteViewModel model);

        /// <summary>
        /// Deletes the customer site.
        /// </summary>
        /// <param name="siteId">The identifier.</param>
        /// <returns></returns>
        Task DeleteCustomerSite(Guid siteId);

        /// <summary>
        /// Creates the customer organization.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateCustomerOrganization(CreateUpdateOrganizationViewModel model);

        /// <summary>
        /// Updates the customer organization.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task UpdateCustomerOrganization(Guid organizationId, CreateUpdateOrganizationViewModel model);

        /// <summary>
        /// Deletes the customer organization.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        Task DeleteCustomerOrganization(Guid organizationId);
    }
}