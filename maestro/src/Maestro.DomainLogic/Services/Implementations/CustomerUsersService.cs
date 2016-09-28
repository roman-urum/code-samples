using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Maestro.Common.Extensions;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.DataAccess.EF.DataAccess;
using Maestro.Domain.Constants;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.TokenService.RequestsResponses;
using Maestro.Domain.Enums;
using Maestro.DomainLogic.Exceptions;
using Maestro.DomainLogic.Services.Interfaces;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// Service to manage relationships between customers and users.
    /// </summary>
    public class CustomerUsersService : ICustomerUsersService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<CustomerUser> customerUsersRepository;
        private readonly IRepository<CustomerUserRole> customerUserRoleRepository;
        private readonly IRepository<CustomerUserSite> customerUserSitesRepository;
        private readonly IUsersDataProvider tokenDataProvider;
        private readonly IUsersService usersService;
        private readonly ICustomersService customersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUsersService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="tokenDataProvider">The token data provider.</param>
        /// <param name="usersService">The users service.</param>
        /// <param name="customersService">The customers service.</param>
        public CustomerUsersService(
            IUnitOfWork unitOfWork,
            IUsersDataProvider tokenDataProvider,
            IUsersService usersService,
            ICustomersService customersService
        )
        {
            this.unitOfWork = unitOfWork;
            customerUsersRepository = this.unitOfWork.CreateGenericRepository<CustomerUser>();
            customerUserRoleRepository = this.unitOfWork.CreateGenericRepository<CustomerUserRole>();
            customerUserSitesRepository = this.unitOfWork.CreateGenericRepository<CustomerUserSite>();
            this.tokenDataProvider = tokenDataProvider;
            this.usersService = usersService;
            this.customersService = customersService;
        }

        /// <summary>
        /// Assign specified user to customer.
        /// </summary>
        /// <param name="customerUser">The customer user.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">customerUser</exception>
        /// <exception cref="DataNotFoundException">Role with name {0} not exists.FormatWith(Roles.CustomerDefaultRole)</exception>
        public async Task<CreateCustomerUserResultDto> CreateCustomerUser(CustomerUser customerUser, string bearerToken)
        {
            var result = new CreateCustomerUserResultDto();

            if (customerUser == null)
            {
                throw new ArgumentNullException("customerUser");
            }

            if (string.IsNullOrEmpty(bearerToken))
            {
                throw new ArgumentNullException("bearerToken");
            }

            var roleEntity = await usersService.GetUserRole(Roles.CustomerUser);

            if (roleEntity == null)
            {
                throw new DataNotFoundException("Role with name {0} not exists".FormatWith(Roles.CustomerUser));
            }

            var existingCustomerUser = await GetCustomerUserByEmail(customerUser.Email, true);

            if (existingCustomerUser != null)
            {
                result.Error = CreateUpdateCustomerUserErrorState.EmailAlreadyExists;
            }

            var existingCustomer = await customersService.GetCustomer(customerUser.CustomerId, bearerToken);

            if (existingCustomer == null)
            {
                if (result.Error.HasValue)
                {
                    result.Error |= CreateUpdateCustomerUserErrorState.CustomerDoesNotExist;
                }
                else
                {
                    result.Error = CreateUpdateCustomerUserErrorState.CustomerDoesNotExist;
                }
            }
            else
            {
                var customerSitesIds = existingCustomer.Sites.Select(s => s.Id).ToList();

                if (customerUser.CustomerUserSites != null && !customerSitesIds.Contains(customerUser.CustomerUserSites.Select(s => s.SiteId).ToArray()))
                {
                    if (result.Error.HasValue)
                    {
                        result.Error |= CreateUpdateCustomerUserErrorState.SitesDoNotExistWithinCustomer;
                    }
                    else
                    {
                        result.Error = CreateUpdateCustomerUserErrorState.SitesDoNotExistWithinCustomer;
                    }
                }
            }

            var customerUserRoles = await GetCustomerUserRolesForCustomer(customerUser.CustomerId);

            if (customerUserRoles.All(cr => cr.Id != customerUser.CustomerUserRoleId))
            {
                if (result.Error.HasValue)
                {
                    result.Error |= CreateUpdateCustomerUserErrorState.CustomerUserRoleDoesNotExistWithinCustomer;
                }
                else
                {
                    result.Error = CreateUpdateCustomerUserErrorState.CustomerUserRoleDoesNotExistWithinCustomer;
                }
            }

            if (!string.IsNullOrEmpty(customerUser.CustomerUserId) ||
                !string.IsNullOrEmpty(customerUser.NationalProviderIdentificator))
            {
                var customerUsers = await GetCustomerUsers(customerUser.CustomerId);

                if (!string.IsNullOrEmpty(customerUser.CustomerUserId) &&
                    customerUsers.Any(cu => cu.CustomerUserId == customerUser.CustomerUserId))
                {
                    if (result.Error.HasValue)
                    {
                        result.Error |= CreateUpdateCustomerUserErrorState.CustomerUserIdAlreadyExists;
                    }
                    else
                    {
                        result.Error = CreateUpdateCustomerUserErrorState.CustomerUserIdAlreadyExists;
                    }
                }

                if (!string.IsNullOrEmpty(customerUser.NationalProviderIdentificator) &&
                    customerUsers.Any(
                        cu => cu.NationalProviderIdentificator == customerUser.NationalProviderIdentificator))
                {
                    if (result.Error.HasValue)
                    {
                        result.Error |= CreateUpdateCustomerUserErrorState.NPIAlreadyExists;
                    }
                    else
                    {
                        result.Error = CreateUpdateCustomerUserErrorState.NPIAlreadyExists;
                    }
                }
            }

            if (result.IsValid)
            {
                var principal = await tokenDataProvider.CreateUser(Mapper.Map<CreatePrincipalModel>(customerUser));

                customerUser.RoleId = roleEntity.Id;
                customerUser.TokenServiceUserId = principal.Id.ToString();
                customerUsersRepository.Insert(customerUser);

                await unitOfWork.SaveAsync();

                var customerUserRole = await GetCustomerUserPermissions(customerUser.Id);

                await usersService.SetUserGroupsByPermissions(customerUserRole.Permissions, customerUser);

                var userSites = customerUser.CustomerUserSites != null ?
                    customerUser.CustomerUserSites.Select(s => s.SiteId).ToList() :
                    new List<Guid>();

                await SetSites(customerUser.Id, userSites);

                result.Id = customerUser.Id;
            }

            return result;
        }

        /// <summary>
        /// Deletes the customer user.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerUserId">The customer user identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteCustomerUser(int customerId, Guid customerUserId)
        {
            var existingCustomerUser = 
                (await customerUsersRepository
                    .FindAsync(cu => cu.CustomerId == customerId && cu.Id == customerUserId && !cu.IsDeleted))
                .SingleOrDefault();

            if (existingCustomerUser != null)
            {
                existingCustomerUser.IsDeleted = true;
                customerUsersRepository.Update(existingCustomerUser);

                await unitOfWork.SaveAsync();

                return await Task.FromResult<bool>(true);
            }

            return await Task.FromResult<bool>(false);
        }

        /// <summary>
        /// Saves info about sites to which user has access.
        /// </summary>
        /// <param name="customerUserId"></param>
        /// <param name="sitesIds"></param>
        /// <returns></returns>
        public async Task SetSites(Guid customerUserId, IList<Guid> sitesIds)
        {
            IEnumerable<CustomerUserSite> currentSites = await this.GetCustomerUserSites(customerUserId);
            var newSitesIds = sitesIds.Where(ns => currentSites.All(s => s.SiteId != ns)).ToList();

            foreach (var siteId in newSitesIds)
            {
                var newSite = new CustomerUserSite
                {
                    SiteId = siteId,
                    CustomerUserId = customerUserId
                };

                customerUserSitesRepository.Insert(newSite);
            }

            var oldSites = currentSites.Where(cs => sitesIds.All(sid => sid != cs.SiteId)).ToList();

            foreach (var currentSite in oldSites)
            {
                customerUserSitesRepository.Delete(currentSite);
            }

            await unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Returns list of sites available for specified user.
        /// </summary>
        /// <param name="customerUserId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CustomerUserSite>> GetCustomerUserSites(Guid customerUserId)
        {
            var result = await customerUserSitesRepository.FindAsync(s => s.CustomerUserId == customerUserId, o => o.OrderBy(c => c.CustomerUser.FirstName));

            return result.ToList();
        }

        public async Task SetCustomerUserRole(Guid customerUserId, Guid customerUserRoleId)
        {
            var customerUser = (await this.customerUsersRepository.FindAsync(u => u.Id == customerUserId)).FirstOrDefault();

            if (customerUser != null)
            {
                customerUser.CustomerUserRoleId = customerUserRoleId;
                await this.unitOfWork.SaveAsync();
            }
        }

        /// <summary>
        /// Returns customer role assigned to user with list 
        /// of permissions included.
        /// </summary>
        /// <param name="customerUserId"></param>
        /// <returns></returns>
        public async Task<CustomerUserRole> GetCustomerUserPermissions(Guid customerUserId)
        {
            CustomerUser customerUser = (await customerUsersRepository.FindAsync(u => u.Id == customerUserId)).FirstOrDefault();

            if (customerUser == null)
            {
                return null;
            }

            var includes = new List<Expression<Func<CustomerUserRole, object>>> { e => e.Permissions };
            var result =
                await
                    this.customerUserRoleRepository.FindAsync(r => r.Id == customerUser.CustomerUserRoleId,
                        null,
                        includes);

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Gets the customer users.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public async Task<IList<CustomerUser>> GetCustomerUsers(int customerId)
        {
            var result = await customerUsersRepository.FindAsync(
                cu => cu.CustomerId == customerId,
                null,
                new List<Expression<Func<CustomerUser, object>>>
                {
                    e => e.CustomerUserSites,
                    e => e.CustomerUserRole
                }
            );

            return result;
        }

        /// <summary>
        /// Gets the customer user by customer user identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerUserId">The customer user identifier.</param>
        /// <returns></returns>
        public async Task<CustomerUser> GetCustomerUserByCustomerUserId(int customerId, string customerUserId)
        {
            return (await customerUsersRepository
                .FindAsync(
                    cu => cu.CustomerId == customerId && cu.CustomerUserId == customerUserId && !cu.IsDeleted,
                    null,
                    new List<Expression<Func<CustomerUser, object>>>
                    {
                        e => e.CustomerUserSites,
                        e => e.CustomerUserRole
                    }
                )
            ).SingleOrDefault();
        }

        /// <summary>
        /// Gets the customer user by npi.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="npi">The npi.</param>
        /// <returns></returns>
        public async Task<CustomerUser> GetCustomerUserByNpi(int customerId, string npi)
        {
            return (await customerUsersRepository
                .FindAsync(cu => cu.CustomerId == customerId && cu.NationalProviderIdentificator == npi && !cu.IsDeleted,
                    null,
                    new List<Expression<Func<CustomerUser, object>>>
                    {
                        e => e.CustomerUserSites,
                        e => e.CustomerUserRole
                    }
                )
            ).SingleOrDefault();
        }

        /// <summary>
        /// Updates the customer user.
        /// </summary>
        /// <param name="customerUser">The customer user.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<UpdateCustomerUserResultDto> UpdateCustomerUser(CustomerUser customerUser, string bearerToken)
        {
            var result = new UpdateCustomerUserResultDto();

            if (customerUser == null)
            {
                throw new ArgumentNullException("customerUser");
            }

            if (string.IsNullOrEmpty(bearerToken))
            {
                throw new ArgumentNullException("bearerToken");
            }

            var existingCustomerUser =
                (await customerUsersRepository.FindAsync(
                    u => u.Id == customerUser.Id && u.CustomerId == customerUser.CustomerId))
                    .SingleOrDefault();

            if (existingCustomerUser == null)
            {
                result.Error = CreateUpdateCustomerUserErrorState.CustomerUserDoesNotExist;

                return result;
            }

            if (
                !String.Equals(existingCustomerUser.Email, customerUser.Email, StringComparison.CurrentCultureIgnoreCase))
            {
                if ((await GetCustomerUserByEmail(customerUser.Email, true)) != null)
                {
                    result.Error = CreateUpdateCustomerUserErrorState.EmailAlreadyExists;
                }
            }

            var existingCustomer = await customersService.GetCustomer(customerUser.CustomerId, bearerToken);

            if (existingCustomer == null)
            {
                if (result.Error.HasValue)
                {
                    result.Error |= CreateUpdateCustomerUserErrorState.CustomerDoesNotExist;
                }
                else
                {
                    result.Error = CreateUpdateCustomerUserErrorState.CustomerDoesNotExist;
                }
            }
            else
            {
                var customerSitesIds = existingCustomer.Sites.Select(s => s.Id).ToList();

                if (customerUser.CustomerUserSites != null &&
                    !customerSitesIds.Contains(customerUser.CustomerUserSites.Select(s => s.SiteId).ToArray()))
                {
                    if (result.Error.HasValue)
                    {
                        result.Error |= CreateUpdateCustomerUserErrorState.SitesDoNotExistWithinCustomer;
                    }
                    else
                    {
                        result.Error = CreateUpdateCustomerUserErrorState.SitesDoNotExistWithinCustomer;
                    }
                }
            }

            if (existingCustomerUser.CustomerUserRoleId != customerUser.CustomerUserRoleId)
            {
                var customerUserRoles = await GetCustomerUserRolesForCustomer(customerUser.CustomerId);

                if (customerUserRoles.All(cr => cr.Id != customerUser.CustomerUserRoleId))
                {
                    if (result.Error.HasValue)
                    {
                        result.Error |= CreateUpdateCustomerUserErrorState.CustomerUserRoleDoesNotExistWithinCustomer;
                    }
                    else
                    {
                        result.Error = CreateUpdateCustomerUserErrorState.CustomerUserRoleDoesNotExistWithinCustomer;
                    }
                }
            }

            if (!string.IsNullOrEmpty(customerUser.CustomerUserId) ||
                !string.IsNullOrEmpty(customerUser.NationalProviderIdentificator))
            {
                var customerUsers =
                    (await GetCustomerUsers(customerUser.CustomerId)).Where(cu => cu.Id != customerUser.Id).ToList();

                if (customerUsers.Any(cu => cu.CustomerUserId == customerUser.CustomerUserId))
                {
                    if (result.Error.HasValue)
                    {
                        result.Error |= CreateUpdateCustomerUserErrorState.CustomerUserIdAlreadyExists;
                    }
                    else
                    {
                        result.Error = CreateUpdateCustomerUserErrorState.CustomerUserIdAlreadyExists;
                    }
                }

                if (
                    customerUsers.Any(
                        cu => cu.NationalProviderIdentificator == customerUser.NationalProviderIdentificator))
                {
                    if (result.Error.HasValue)
                    {
                        result.Error |= CreateUpdateCustomerUserErrorState.NPIAlreadyExists;
                    }
                    else
                    {
                        result.Error = CreateUpdateCustomerUserErrorState.NPIAlreadyExists;
                    }
                }
            }

            if (result.IsValid)
            {
                // Updates user's email in token service
                var updateRequest = new UpdatePrincipalModel
                {
                    Username = customerUser.Email,
                    FirstName = customerUser.FirstName,
                    LastName = customerUser.LastName
                };

                await
                    tokenDataProvider.UpdatePrincipals(Guid.Parse(existingCustomerUser.TokenServiceUserId),
                        updateRequest);

                existingCustomerUser.IsEnabled = customerUser.IsEnabled;
                existingCustomerUser.FirstName = customerUser.FirstName;
                existingCustomerUser.LastName = customerUser.LastName;
                existingCustomerUser.Email = customerUser.Email;
                existingCustomerUser.Phone = customerUser.Phone;
                existingCustomerUser.State = customerUser.State;
                existingCustomerUser.City = customerUser.City;
                existingCustomerUser.Address1 = customerUser.Address1;
                existingCustomerUser.Address2 = customerUser.Address2;
                existingCustomerUser.Address3 = customerUser.Address3;
                existingCustomerUser.ZipCode = customerUser.ZipCode;
                existingCustomerUser.NationalProviderIdentificator =
                    customerUser.NationalProviderIdentificator;
                existingCustomerUser.CustomerUserId = customerUser.CustomerUserId;

                await unitOfWork.SaveAsync();

                result.User = existingCustomerUser;

                var userSites = customerUser.CustomerUserSites != null
                    ? customerUser.CustomerUserSites.Select(s => s.SiteId).ToList()
                    : new List<Guid>();

                await SetSites(existingCustomerUser.Id, userSites);

                if (customerUser.CustomerUserRoleId.HasValue)
                {
                    await SetCustomerUserRole(existingCustomerUser.Id, customerUser.CustomerUserRoleId.Value);
                }

                var customerUserRole = await GetCustomerUserPermissions(existingCustomerUser.Id);

                await usersService.SetUserGroupsByPermissions(customerUserRole.Permissions, existingCustomerUser);
            }

            return result;
        }

        /// <summary>
        /// Gets the customer user roles for customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public async Task<IList<CustomerUserRole>> GetCustomerUserRolesForCustomer(int customerId)
        {
            return await customerUserRoleRepository.FindAsync(r => r.CustomerId == customerId);
        }

        /// <summary>
        /// Gets the customer user by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        public async Task<CustomerUser> GetCustomerUserByEmail(string email, bool includeDeleted = false)
        {
            if (includeDeleted)
            {
                return (await customerUsersRepository
                    .FindAsync(cu => cu.Email.ToLower() == email.ToLower()))
                    .SingleOrDefault();
            }

            return (await customerUsersRepository
                .FindAsync(cu => cu.Email.ToLower() == email.ToLower() && !cu.IsDeleted))
                .SingleOrDefault();
        }

        /// <summary>
        /// Gets the care managers.
        /// </summary>
        /// <param name="customerId">The identifier.</param>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public async Task<IList<CustomerUser>> GetCareManagers(int customerId, Guid siteId)
        {
            var users = await customerUsersRepository
                .FindAsync(
                    u =>
                        u.CustomerId == customerId &&
                        u.CustomerUserSites.Any(s => s.SiteId == siteId) &&
                        u.IsEnabled,
                    o => o
                        .OrderBy(e => e.LastName)
                        .ThenBy(e => e.FirstName),
                    new List<Expression<Func<CustomerUser, object>>>
                    {
                        e => e.Role,
                        e => e.CustomerUserSites,
                        e => e.CustomerUserRole.Permissions
                    }
                );

            return users.ToList();
        }
    }
}