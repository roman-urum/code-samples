using System;
using System.Threading.Tasks;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Domain.Entities;

namespace CustomerService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IOrganizationsService.
    /// </summary>
    public interface IOrganizationsService
    {
        /// <summary>
        /// Gets the organizations.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResult<Organization>> GetOrganizations(int customerId, OrganizationSearchDto request);

        /// <summary>
        /// Gets the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        Task<Organization> GetOrganization(int customerId, Guid organizationId);

        /// <summary>
        /// Creates the organization.
        /// </summary>
        /// <param name="organization">The organization.</param>
        /// <returns></returns>
        Task<OperationResultDto<Guid, OrganizationStatus>> CreateOrganization(Organization organization);

        /// <summary>
        /// Updates the organization.
        /// </summary>
        /// <param name="organization">The organization.</param>
        /// <param name="isDeleteRequested">The is delete requested.</param>
        /// <returns></returns>
        Task<OrganizationStatus> UpdateOrganization(Organization organization, bool? isDeleteRequested);

        /// <summary>
        /// Deletes the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        Task<OrganizationStatus> DeleteOrganization(int customerId, Guid organizationId);
    }
}