using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Maestro.Domain;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.CustomerService;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.Web.Areas.Customer.Models.Settings.Sites;
using Maestro.Web.Exceptions;
using Maestro.Web.Resources;

namespace Maestro.Web.Areas.Customer.Managers.Implementations
{
    /// <summary>
    /// SettingsControllerManager.SitesAndOrgs
    /// </summary>
    /// <seealso cref="Maestro.Web.Areas.Customer.Managers.Interfaces.ISettingsControllerManager" />
    public partial class SettingsControllerManager
    {
        /// <summary>
        /// Creates new site for current customer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<PostResponseDto<Guid>> CreateCustomerSite(CreateUpdateSiteViewModel model)
        {
            var site = Mapper.Map<CreateUpdateSiteViewModel, SiteRequestDto>(model);

            return customersService.CreateSite(customerContext.Customer.Id, site, authDataStorage.GetToken());
        }

        /// <summary>
        /// Updates customer site data.
        /// </summary>
        /// <returns></returns>
        public Task UpdateCustomerSite(Guid siteId, CreateUpdateSiteViewModel model)
        {
            var site = Mapper.Map<CreateUpdateSiteViewModel, SiteRequestDto>(model);

            return customersService.UpdateSite(customerContext.Customer.Id, siteId, site, authDataStorage.GetToken());
        }

        /// <summary>
        /// Deletes the customer site.
        /// </summary>
        /// <param name="siteId">The identifier.</param>
        /// <returns></returns>
        public async Task DeleteCustomerSite(Guid siteId)
        {
            var customerId = customerContext.Customer.Id;
            var bearerToken = authDataStorage.GetToken();

            var getCareManagersTask = customerUsersService.GetCareManagers(customerId, siteId);

            var getPatientsTask = patientsService.GetPatients(
                bearerToken,
                new PatientsSearchDto()
                {
                    CustomerId = customerId,
                    SiteId = siteId,
                    IsBrief = true,
                    Skip = 0,
                    Take = int.MaxValue
                }
            );

            await Task.WhenAll(getCareManagersTask, getPatientsTask);

            if (getCareManagersTask.Result.Any() || getPatientsTask.Result.Results.Count != 0)
            {
                throw new LogicException(GlobalStrings.CustomerSettings_SitesAndOrgs_DeleteSiteForbidden);
            }

            await customersService.DeleteSite(customerId, siteId, bearerToken);
        }

        /// <summary>
        /// Creates the customer organization.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public Task<PostResponseDto<Guid>> CreateCustomerOrganization(CreateUpdateOrganizationViewModel model)
        {
            var organization = Mapper.Map<CreateUpdateOrganizationViewModel, OrganizationRequestDto>(model);

            return customersService.CreateOrganization(customerContext.Customer.Id, organization, authDataStorage.GetToken());
        }

        /// <summary>
        /// Updates the customer organization.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public Task UpdateCustomerOrganization(Guid organizationId, CreateUpdateOrganizationViewModel model)
        {
            var organization = Mapper.Map<CreateUpdateOrganizationViewModel, OrganizationRequestDto>(model);

            return customersService.UpdateOrganization(customerContext.Customer.Id, organizationId, organization, authDataStorage.GetToken());
        }

        /// <summary>
        /// Deletes the customer organization.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        public async Task DeleteCustomerOrganization(Guid organizationId)
        {
            var customerId = customerContext.Customer.Id;
            var bearerToken = authDataStorage.GetToken();

            var customerOrganizationNestedSites = await customersService
                .GetSites(
                    customerId,
                    new SiteSearchDto()
                    {
                        OrganizationId = organizationId,
                        IncludeArchived = false
                    },
                    bearerToken
                );

            if (customerOrganizationNestedSites.Any())
            {
                throw new LogicException(GlobalStrings.CustomerSettings_SitesAndOrgs_DeleteOrganizationForbidden);
            }

            await customersService.DeleteOrganization(customerId, organizationId, authDataStorage.GetToken());
        }

        #region Private methods

        /// <summary>
        /// Uploads logo image to customer service.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task<string> SaveLogoImage(HttpPostedFileBase file)
        {
            var image = Image.FromStream(file.InputStream);
            var memoryStream = new MemoryStream();

            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

            var data = memoryStream.ToArray();

            var uploadLogoRequest = new FileDto
            {
                ContentType = file.ContentType,
                FileName = file.FileName,
                FileData = Convert.ToBase64String(data)
            };

            return await customersService.UploadLogo(uploadLogoRequest, authDataStorage.GetToken());
        }

        #endregion
    }
}