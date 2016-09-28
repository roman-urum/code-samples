using System;
using System.Threading.Tasks;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Web.Api.Models.Dtos;
using CustomerService.Web.Api.Models.Dtos.Organizations;

namespace CustomerService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// IOrganizationsControllerHelper.
    /// </summary>
    public interface IOrganizationsControllerHelper
    {
        /// <summary>
        /// Gets customers.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResultDto<OrganizationResponseDto>> GetOrganizations(int customerId, OrganizationSearchDto request);

        /// <summary>
        /// Gets the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        Task<OperationResultDto<OrganizationResponseDto, OrganizationStatus>> GetOrganization(int customerId, Guid organizationId);

        /// <summary>
        /// Creates the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<OperationResultDto<Guid, OrganizationStatus>> CreateOrganization(int customerId, CreateOrganizationRequestDto request);

        /// <summary>
        /// Updates the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<OrganizationStatus> UpdateOrganization(int customerId, Guid organizationId, UpdateOrganizationRequestDto request);

        /// <summary>
        /// Deletes the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        Task<OrganizationStatus> DeleteOrganization(int customerId, Guid organizationId);
    }
}