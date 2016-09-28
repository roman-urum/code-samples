using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CustomerService.Common.Helpers;
using CustomerService.DataAccess;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Domain.Entities;
using CustomerService.DomainLogic.Services.Interfaces;

namespace CustomerService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// OrganizationsService.
    /// </summary>
    public class OrganizationsService : IOrganizationsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Organization> organizationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationsService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public OrganizationsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.organizationRepository = this.unitOfWork.CreateGenericRepository<Organization>();
        }

        /// <summary>
        /// Gets the organizations.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResult<Organization>> GetOrganizations(int customerId, OrganizationSearchDto request)
        {
            Expression<Func<Organization, bool>> expression = o => o.CustomerId == customerId;

            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.Q))
                {
                    var terms = request.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(o => o.Name.Contains(term));
                    }
                }

                if (!request.IncludeArchived)
                {
                    expression = expression.And(o => !o.IsDeleted);
                }
            }

            return await organizationRepository
                .FindPagedAsync(
                    expression,
                    o => o.OrderBy(e => e.Id),
                    null,
                    request != null ? request.Skip : (int?)null,
                    request != null ? request.Take : (int?)null
                );
        }

        /// <summary>
        /// Gets the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        public async Task<Organization> GetOrganization(int customerId, Guid organizationId)
        {
            return (await organizationRepository
                .FindAsync(
                    o => o.CustomerId == customerId &&
                    o.Id == organizationId))
                .SingleOrDefault();
        }

        /// <summary>
        /// Creates the organization.
        /// </summary>
        /// <param name="organization">The organization.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Guid, OrganizationStatus>> CreateOrganization(Organization organization)
        {
            OrganizationStatus validationResult = 0;

            var existingOrganizations = await organizationRepository.FindAsync(
                o => o.CustomerId == organization.CustomerId &&
                (o.Id == organization.ParentOrganizationId || o.Name.ToLower() == organization.Name.ToLower())
            );

            if (existingOrganizations.Any(o => o.Name.ToLower() == organization.Name.ToLower()))
            {
                validationResult |= OrganizationStatus.NameConflict;
            }

            if (organization.ParentOrganizationId.HasValue && existingOrganizations.All(o => o.Id != organization.ParentOrganizationId))
            {
                validationResult |= OrganizationStatus.ParentNotFound;
            }

            if (validationResult > 0)
            {
                return new OperationResultDto<Guid, OrganizationStatus>()
                {
                    Status = validationResult
                };
            }

            organizationRepository.Insert(organization);
            await unitOfWork.SaveAsync();

            return new OperationResultDto<Guid, OrganizationStatus>()
            {
                Content = organization.Id,
                Status = OrganizationStatus.Success
            };
        }

        /// <summary>
        /// Updates the organization.
        /// </summary>
        /// <param name="organization">The organization.</param>
        /// <param name="isDeleteRequested">The is delete requested.</param>
        /// <returns></returns>
        public async Task<OrganizationStatus> UpdateOrganization(Organization organization, bool? isDeleteRequested)
        {
            var existingOrganizations = await organizationRepository.FindAsync(o => o.CustomerId == organization.CustomerId);
            var organizationToUpdate = existingOrganizations.SingleOrDefault(o => o.Id == organization.Id);

            if (organizationToUpdate == null)
            {
                return OrganizationStatus.NotFound;
            }

            if (organizationToUpdate.Name.ToLower() != organization.Name.ToLower() &&
                existingOrganizations.Any(o => o.Name.ToLower() == organization.Name.ToLower() && o.Id != organization.Id))
            {
                return OrganizationStatus.NameConflict;
            }

            Mapper.Map(organization, organizationToUpdate, typeof(Organization), typeof(Organization));

            if (isDeleteRequested.HasValue)
            {
                organizationToUpdate.IsDeleted = isDeleteRequested.Value;
            }

            organizationRepository.Update(organizationToUpdate);
            await unitOfWork.SaveAsync();

            return OrganizationStatus.Success;
        }

        /// <summary>
        /// Deletes the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        public async Task<OrganizationStatus> DeleteOrganization(int customerId, Guid organizationId)
        {
            var existingOrganization =
                await GetOrganization(customerId, organizationId);

            if (existingOrganization != null)
            {
                existingOrganization.IsDeleted = true;

                await unitOfWork.SaveAsync();

                return OrganizationStatus.Success;
            }

            return OrganizationStatus.NotFound;
        }
    }
}