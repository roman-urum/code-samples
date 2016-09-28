using System;
using System.Threading.Tasks;
using AutoMapper;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Domain.Entities;
using CustomerService.DomainLogic.Services.Interfaces;
using CustomerService.Web.Api.Helpers.Interfaces;
using CustomerService.Web.Api.Models.Dtos;
using CustomerService.Web.Api.Models.Dtos.Organizations;

namespace CustomerService.Web.Api.Helpers.Implemetations
{
    /// <summary>
    /// OrganizationsControllerHelper.
    /// </summary>
    public class OrganizationsControllerHelper : IOrganizationsControllerHelper
    {
        private readonly IOrganizationsService organizationsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersControllerHelper" /> class.
        /// </summary>
        /// <param name="organizationsService">The organizations service.</param>
        public OrganizationsControllerHelper(IOrganizationsService organizationsService)
        {
            this.organizationsService = organizationsService;
        }

        /// <summary>
        /// Gets customers.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResultDto<OrganizationResponseDto>> GetOrganizations(int customerId, OrganizationSearchDto request)
        {
            var result = await organizationsService.GetOrganizations(customerId, request);

            return Mapper.Map<PagedResult<Organization>, PagedResultDto<OrganizationResponseDto>>(result);
        }

        /// <summary>
        /// Gets the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<OrganizationResponseDto, OrganizationStatus>> GetOrganization(
            int customerId,
            Guid organizationId
        )
        {
            var result = await organizationsService.GetOrganization(customerId, organizationId);

            if (result == null)
            {
                return await Task.FromResult(
                    new OperationResultDto<OrganizationResponseDto, OrganizationStatus>()
                    {
                        Status = OrganizationStatus.NotFound
                    }
                );
            }

            return await Task.FromResult(
                new OperationResultDto<OrganizationResponseDto, OrganizationStatus>()
                {
                    Status = OrganizationStatus.Success,
                    Content = Mapper.Map<Organization, OrganizationResponseDto>(result)
                }
            );
        }

        /// <summary>
        /// Creates the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public Task<OperationResultDto<Guid, OrganizationStatus>> CreateOrganization(
            int customerId,
            CreateOrganizationRequestDto request
        )
        {
            var organization = Mapper.Map<CreateOrganizationRequestDto, Organization>(request);
            organization.CustomerId = customerId;

            return organizationsService.CreateOrganization(organization);
        }

        /// <summary>
        /// Updates the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public Task<OrganizationStatus> UpdateOrganization(
            int customerId,
            Guid organizationId,
            UpdateOrganizationRequestDto request
        )
        {
            var organization = Mapper.Map<UpdateOrganizationRequestDto, Organization>(request);
            organization.Id = organizationId;
            organization.CustomerId = customerId;

            return organizationsService.UpdateOrganization(organization, request.IsArchived);
        }

        /// <summary>
        /// Deletes the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        public Task<OrganizationStatus> DeleteOrganization(int customerId, Guid organizationId)
        {
            return organizationsService.DeleteOrganization(customerId, organizationId);
        }
    }
}