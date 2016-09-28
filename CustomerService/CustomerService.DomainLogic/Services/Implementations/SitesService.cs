using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CustomerService.Common.Helpers;
using CustomerService.DataAccess;
using CustomerService.DataAccess.Extensions;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Domain.Entities;
using CustomerService.DomainLogic.Services.Interfaces;

namespace CustomerService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// CustomerSiteService.
    /// </summary>
    public class SitesService : ISiteService
    {
        private readonly int MaestroAdminsCustomerId = 1;

        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Site> siteRepository;
        private readonly IRepository<Customer> customerRepository;
        private readonly IRepository<CategoryOfCare> categoryOfCareRepository;
        private readonly IRepository<Organization> organizationRepository;

        private readonly List<Expression<Func<Site, object>>> SiteIncludes =
            new List<Expression<Func<Site, object>>>
            {
                e => e.CategoriesOfCare
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="SitesService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public SitesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.siteRepository = this.unitOfWork.CreateGenericRepository<Site>();
            this.customerRepository = this.unitOfWork.CreateGenericRepository<Customer>();
            this.categoryOfCareRepository = this.unitOfWork.CreateGenericRepository<CategoryOfCare>();
            this.organizationRepository = this.unitOfWork.CreateGenericRepository<Organization>();
        }

        /// <summary>
        /// Gets all sites.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedResult<Site>> GetSites(int customerId, SiteSearchDto request = null)
        {
            Expression<Func<Site, bool>> expression = s => s.CustomerId == customerId;

            if (request != null)
            {
                if (request.OrganizationId.HasValue)
                {
                    // Retrieving all customer's organizations to skip additional calls
                    // to the database during building tree
                    var customerOrganizations = await organizationRepository
                        .FindAsync(
                            c => c.CustomerId == customerId,
                            null,
                            new List<Expression<Func<Organization, object>>>
                            {
                                o => o.Sites,
                                o => o.ParentOrganization,
                                o => o.ChildOrganizations
                            }
                        );

                    var result = new List<Site>();
                    long total = 0;

                    var targetOrganization =
                        customerOrganizations.FirstOrDefault(o => o.Id == request.OrganizationId.Value);

                    if (targetOrganization != null)
                    {
                        var allOrganizations = GetAllSubOrganizations(targetOrganization);

                        foreach (var organization in allOrganizations)
                        {
                            result.AddRange(organization.Sites);
                        }

                        if (!string.IsNullOrEmpty(request.Q))
                        {
                            var terms = request.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                            foreach (var term in terms)
                            {
                                expression = expression.And(s => s.Name.Contains(term));
                            }
                        }

                        if (!request.IncludeArchived)
                        {
                            expression = expression.And(s => !s.IsDeleted);
                        }

                        result = result
                            .Where(expression.Compile())
                            .ToList();

                        total = result.LongCount();

                        result = result
                            .OrderBy(s => s.Name)
                            .Skip(request.Skip)
                            .Take(request.Take)
                            .ToList();
                    }

                    return new PagedResult<Site>()
                    {
                        Results = result,
                        Total = total
                    };
                }

                if (!string.IsNullOrEmpty(request.Q))
                {
                    var terms = request.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(s => s.Name.Contains(term));
                    }
                }

                if (!request.IncludeArchived)
                {
                    expression = expression.And(s => !s.IsDeleted);
                }
            }

            return await siteRepository
                .FindPagedAsync(
                    expression,
                    o => o.OrderBy(e => e.Name),
                    SiteIncludes,
                    request != null ? request.Skip : (int?) null,
                    request != null ? request.Take : (int?) null
                );
        }

        /// <summary>
        /// Gets the site by identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <returns></returns>
        public async Task<Site> GetSiteById(int customerId, Guid siteId)
        {
            if (customerId != MaestroAdminsCustomerId)
            {
                return (await siteRepository
                    .FindAsync(
                        s => s.Id == siteId &&
                             s.CustomerId == customerId,
                        o => o.OrderBy(e => e.Id),
                        SiteIncludes
                    )).FirstOrDefault();
            }

            return (await siteRepository
                .FindAsync(
                    s => s.Id == siteId,
                    o => o.OrderBy(e => e.Id),
                    SiteIncludes
                )).FirstOrDefault();
        }

        /// <summary>
        /// Creates the site.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Guid, SiteStatus>> CreateSite(Site site)
        {
            var validationResult = await ValidateSite(site);

            if (validationResult > 0)
            {
                return new OperationResultDto<Guid, SiteStatus>()
                {
                    Status = validationResult
                };
            }

            siteRepository.Insert(site);

            await unitOfWork.SaveAsync();

            return new OperationResultDto<Guid, SiteStatus>()
            {
                Content = site.Id,
                Status = SiteStatus.Success
            };
        }

        /// <summary>
        /// Updates the site asynchronous.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="isDeleteRequested">The is delete requested.</param>
        /// <returns></returns>
        public async Task<SiteStatus> UpdateSite(Site site, bool? isDeleteRequested)
        {
            var existingSite = (await siteRepository
                .FindAsync(s => s.Id == site.Id && s.CustomerId == site.CustomerId))
                .SingleOrDefault();

            if (existingSite == null)
            {
                return SiteStatus.NotFound;
            }

            var validationResult = await ValidateSite(site);

            if (validationResult > 0)
            {
                return validationResult;
            }

            existingSite.IsActive = site.IsActive;
            existingSite.IsPublished = site.IsPublished;
            existingSite.Name = site.Name;
            existingSite.NationalProviderIdentificator = site.NationalProviderIdentificator;
            existingSite.State = site.State;
            existingSite.ZipCode = site.ZipCode;
            existingSite.Address1 = site.Address1;
            existingSite.Address2 = site.Address2;
            existingSite.Address3 = site.Address3;
            existingSite.City = site.City;
            existingSite.ContactPhone = site.ContactPhone;
            existingSite.CustomerSiteId = site.CustomerSiteId;
            existingSite.ParentOrganizationId = site.ParentOrganizationId;

            if (isDeleteRequested.HasValue)
            {
                existingSite.IsDeleted = isDeleteRequested.Value;
            }

            existingSite.CategoriesOfCare.RemoveRange(existingSite.CategoriesOfCare.ToList());
            existingSite.CategoriesOfCare.AddRange(site.CategoriesOfCare);

            siteRepository.Update(existingSite);

            await unitOfWork.SaveAsync();

            return SiteStatus.Success;
        }

        /// <summary>
        /// Gets the name of the customer site by.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <returns></returns>
        public async Task<Site> GetCustomerSiteByName(int customerId, string siteName)
        {
            var site = await siteRepository
                .FindAsync(
                    s => s.CustomerId == customerId && s.Name.ToLower() == siteName.ToLower(),
                    o => o.OrderBy(s => s.Id),
                    SiteIncludes
                );

            return site.FirstOrDefault();
        }

        /// <summary>
        /// Gets the name of the site by.
        /// </summary>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public async Task<Site> GetSiteByName(string siteName, int? customerId = null)
        {
            if (customerId.HasValue && customerId.Value != MaestroAdminsCustomerId)
            {
                return (await siteRepository
                    .FindAsync(
                        s => s.Name.ToLower() == siteName.ToLower() &&
                             s.CustomerId == customerId,
                        o => o.OrderBy(s => s.Id),
                        SiteIncludes
                    )).FirstOrDefault();
            }

            return (await siteRepository
                .FindAsync(
                    s => s.Name.ToLower() == siteName.ToLower(),
                    o => o.OrderBy(s => s.Id),
                    SiteIncludes
                )).FirstOrDefault();
        }

        /// <summary>
        /// Gets the site by customer identifier and site NPI.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteNpi">The site NPI.</param>
        /// <returns></returns>
        public async Task<Site> GetSiteByNpi(int customerId, string siteNpi)
        {
            var site = await siteRepository.FindAsync(
                s => s.CustomerId == customerId &&
                     s.NationalProviderIdentificator.ToLower() == siteNpi.ToLower(),
                o => o.OrderBy(s => s.Id),
                SiteIncludes
                );

            return site.SingleOrDefault();
        }

        /// <summary>
        /// Gets the site by customer identifier and customer site identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerSiteId">The customer site identifier.</param>
        /// <returns></returns>
        public async Task<Site> GetSiteByCustomerSiteId(int customerId, string customerSiteId)
        {
            var site = await siteRepository.FindAsync(
                s => s.CustomerId == customerId &&
                     s.CustomerSiteId.ToLower() == customerSiteId.ToLower(),
                o => o.OrderBy(s => s.Id),
                SiteIncludes
                );

            return site.SingleOrDefault();
        }

        /// <summary>
        /// Deletes the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteSite(int customerId, Guid siteId)
        {
            var existingSite = await GetSiteById(customerId, siteId);

            if (existingSite != null)
            {
                existingSite.IsDeleted = true;

                await unitOfWork.SaveAsync();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Validates the site.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <returns></returns>
        private async Task<SiteStatus> ValidateSite(Site site)
        {
            SiteStatus validationResult = 0;

            var existingCustomer = (await customerRepository.FindAsync(c => c.Id == site.CustomerId)).SingleOrDefault();

            if (existingCustomer == null)
            {
                validationResult |= SiteStatus.CustomerNotFound;
            }

            if (site.ParentOrganizationId.HasValue)
            {
                var existingOrganization = (await organizationRepository
                    .FindAsync(o => o.CustomerId == site.CustomerId && o.Id == site.ParentOrganizationId.Value)
                    ).SingleOrDefault();

                if (existingOrganization == null)
                {
                    validationResult |= SiteStatus.OrganizationNotFound;
                }
            }

            if (!string.IsNullOrEmpty(site.Name) && (
                site.IsNew
                    ? (await siteRepository.FindAsync(s => s.CustomerId == site.CustomerId && s.Name.ToLower() == site.Name.ToLower())).Any()
                    : (await siteRepository.FindAsync(s => s.CustomerId == site.CustomerId && s.Name.ToLower() == site.Name.ToLower() &&
                                                           s.Id != site.Id)).Any())
                )
            {
                validationResult |= SiteStatus.NameConflict;
            }

            if (!string.IsNullOrEmpty(site.NationalProviderIdentificator) && (
                site.IsNew
                    ? (await siteRepository.FindAsync(s => s.CustomerId == site.CustomerId &&
                                                           s.NationalProviderIdentificator ==
                                                           site.NationalProviderIdentificator)).Any()
                    : (await siteRepository.FindAsync(s => s.CustomerId == site.CustomerId &&
                                                           s.NationalProviderIdentificator ==
                                                           site.NationalProviderIdentificator && s.Id != site.Id)).Any())
                )
            {
                validationResult |= SiteStatus.NPIConflict;
            }

            if (!string.IsNullOrEmpty(site.CustomerSiteId) && (
                site.IsNew
                    ? (await siteRepository.FindAsync(s => s.CustomerId == site.CustomerId &&
                                                           s.CustomerSiteId == site.CustomerSiteId)).Any()
                    : (await siteRepository.FindAsync(s => s.CustomerId == site.CustomerId &&
                                                           s.CustomerSiteId == site.CustomerSiteId && s.Id != site.Id))
                        .Any())
                )
            {
                validationResult |= SiteStatus.CustomerSiteIdConflict;
            }

            if (site.CategoriesOfCare != null && site.CategoriesOfCare.Any())
            {
                var categoriesOfCareIds = site.CategoriesOfCare.Select(c => c.Id).Distinct().ToList();

                var existingCategoriesOfCare = await categoryOfCareRepository
                    .FindAsync(c => c.CustomerId == site.CustomerId && categoriesOfCareIds.Contains(c.Id));

                if (categoriesOfCareIds.Count != existingCategoriesOfCare.Count)
                {
                    validationResult |= SiteStatus.CategoryOfCareConflict;
                }
                else
                {
                    site.CategoriesOfCare = existingCategoriesOfCare;
                }
            }

            return validationResult;
        }

        private IList<Organization> GetAllSubOrganizations(Organization organization)
        {
            var result = new List<Organization>();

            result.Add(organization);

            foreach (var childOrganization in organization.ChildOrganizations)
            {
                result.AddRange(GetAllSubOrganizations(childOrganization));
            }

            return result;
        }
    }
}