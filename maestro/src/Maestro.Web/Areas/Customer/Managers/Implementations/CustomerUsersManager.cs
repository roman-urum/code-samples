using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Maestro.Common.Extensions;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos;
using Maestro.DomainLogic.Exceptions;
using Maestro.DomainLogic.Services.Interfaces;
using Maestro.Web.Areas.Customer.Managers.Interfaces;
using Maestro.Web.Areas.Customer.Models.Settings.CustomerUsers;
using Maestro.Web.Exceptions;
using Maestro.Web.Extensions;
using Maestro.Web.Managers.Interfaces;
using Maestro.Web.Models.Api.Dtos.Entities;
using Maestro.Web.Models.Api.Dtos.RequestsResponses;
using Maestro.Web.Security;

namespace Maestro.Web.Areas.Customer.Managers.Implementations
{
    /// <summary>
    /// Service to manage users in customer area.
    /// </summary>
    public class CustomerUsersManager : ICustomerUsersManager
    {
        private readonly IUsersService usersService;
        private readonly ICustomerUsersService customerUsersService;
        private readonly IEmailManager emailManager;
        private readonly ICustomersService customersService;
        private readonly IAuthDataStorage authDataStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUsersManager" /> class.
        /// </summary>
        /// <param name="usersService">The users service.</param>
        /// <param name="customersService">The customers service.</param>
        /// <param name="customerUsersService">The customer users service.</param>
        /// <param name="emailManager">The email manager.</param>
        /// <param name="authDataStorage">The authentication data storage.</param>
        public CustomerUsersManager(
            IUsersService usersService,
            ICustomersService customersService,
            ICustomerUsersService customerUsersService,
            IEmailManager emailManager,
            IAuthDataStorage authDataStorage)
        {
            this.usersService = usersService;
            this.customerUsersService = customerUsersService;
            this.customersService = customersService;
            this.emailManager = emailManager;
            this.authDataStorage = authDataStorage;
        }

        /// <summary>
        /// Creates new user record in database and token service.
        /// User enabled by default.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<CreateCustomerUserResultDto> CreateCustomerUser(CustomerUserViewModel model)
        {
            var customerUser = Mapper.Map<CustomerUserViewModel, CustomerUser>(model);
            var bearerToken = this.authDataStorage.GetToken();

            customerUser.CustomerId = CustomerContext.Current.Customer.Id;
            
            var result = await customerUsersService.CreateCustomerUser(customerUser, bearerToken);

            if (result.IsValid && !model.DoNotSendInvitation)
            {
                var passwordExpirationDays = CustomerContext.Current.Customer.PasswordExpirationDays;

                await emailManager.SendActivationEmail(customerUser, passwordExpirationDays);
            }

            return result;
        }

        /// <summary>
        /// Creates customer user using data of dto for Users API.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<CreateCustomerUserResultDto> CreateCustomerUser(
            int customerId, 
            CreateCustomerUserRequestDto request
        )
        {
            string bearerToken = HttpContext.Current.Request.GetAuthorizationToken();

            var customerUser = Mapper.Map<CreateCustomerUserRequestDto, CustomerUser>(request);
            customerUser.CustomerId = customerId;

            var result = await customerUsersService.CreateCustomerUser(customerUser, bearerToken);

            if (result.IsValid && !request.DoNotSendInvitation)
            {
                var passwordExpirationDays = (await customersService.GetCustomer(customerId, bearerToken)).PasswordExpirationDays;

                await emailManager.SendActivationEmail(customerUser, passwordExpirationDays);
            }

            return result;
        }

        /// <summary>
        /// Returns list of users assigned to specified customer
        /// with info about available sites.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<List<CustomerUserListViewModel>> GetCustomerUsers(int customerId)
        {
            List<CustomerUser> users = (await customerUsersService.GetCustomerUsers(customerId)).ToList();

            return Mapper.Map<List<CustomerUserListViewModel>>(users);
        }

        /// <summary>
        /// Returns list of user roles for current customer.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CustomerUserRoleViewModel>> GetCustomerUserRoles()
        {
            var result =
                await this.customerUsersService.GetCustomerUserRolesForCustomer(CustomerContext.Current.Customer.Id);

            return Mapper.Map<IList<CustomerUserRoleViewModel>>(result);
        }

        /// <summary>
        /// Updates customer user data and access to sites.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<UpdateCustomerUserResultDto> UpdateCustomerUser(Guid userId, CustomerUserViewModel model)
        {
            var bearerToken = authDataStorage.GetToken();
            var customerUser = Mapper.Map<CustomerUserViewModel, CustomerUser>(model);

            customerUser.Id = userId;
            customerUser.CustomerId = CustomerContext.Current.Customer.Id;

            return await customerUsersService.UpdateCustomerUser(customerUser, bearerToken);
        }

        /// <summary>
        /// Updates the customer user.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerUserId">The customer user identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<UpdateCustomerUserResultDto> UpdateCustomerUser(
            int customerId, 
            Guid customerUserId,
            UpdateCustomerUserRequestDto request
        )
        {
            string bearerToken = HttpContext.Current.Request.GetAuthorizationToken();
            var customerUser = Mapper.Map<UpdateCustomerUserRequestDto, CustomerUser>(request);

            customerUser.Id = customerUserId;

            return await customerUsersService.UpdateCustomerUser(customerUser, bearerToken);
        }

        /// <summary>
        /// Deletes the customer user.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerUserId">The customer user identifier.</param>
        /// <returns></returns>
        public Task<bool> DeleteCustomerUser(int customerId, Guid customerUserId)
        {
            return customerUsersService.DeleteCustomerUser(customerId, customerUserId);
        }

        /// <summary>
        /// Updates isenabled field for user with specified id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        public async Task SetEnabledState(Guid userId, bool isEnabled)
        {
            var user = await usersService.GetUser(userId);

            if (user == null)
            {
                throw new DataNotFoundException("user", userId);
            }

            if (!await IsCustomerUser(userId))
            {
                throw new ServiceUsageException(
                    "User with id {0} cannot be modified in current request.".FormatWith(userId));
            }

            user.IsEnabled = isEnabled;

            await usersService.EditUser(user, CustomerContext.Current.Customer.Id);
        }

        /// <summary>
        /// Determines whether customer user NPI is unique within a customer user.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="npi">The npi.</param>
        /// <param name="databaseCustomerUserId">The database customer user identifier.</param>
        /// <returns></returns>
        public async Task<bool> IsNpiUniqueWithinCustomerUser(
            int customerId,
            string npi,
            Guid? databaseCustomerUserId = null
        )
        {
            var customerUsers = await this.customerUsersService.GetCustomerUsers(customerId);

            if (databaseCustomerUserId != null)
            {
                return customerUsers
                    .Where(cu => cu.Id != databaseCustomerUserId)
                    .All(cu => cu.NationalProviderIdentificator != npi);
            }

            return customerUsers
                .All(cu => cu.NationalProviderIdentificator != npi);
        }

        /// <summary>
        /// Determines whether customer user id is unique within a customer user.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerUserId">The customer user identifier.</param>
        /// <param name="databaseCustomerUserId">The database customer user identifier.</param>
        /// <returns></returns>
        public async Task<bool> IsCustomerUserIdUniqueWithinCustomerUser(
            int customerId,
            string customerUserId,
            Guid? databaseCustomerUserId = null
        )
        {
            var customerUsers = await this.customerUsersService.GetCustomerUsers(customerId);

            if (databaseCustomerUserId != null)
            {
                return customerUsers
                    .Where(cu => cu.Id != databaseCustomerUserId)
                    .All(cu => cu.CustomerUserId != customerUserId);
            }

            return customerUsers
                .All(cu => cu.CustomerUserId != customerUserId);
        }

        /// <summary>
        /// Gets the customer user by customer user identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerUserId">The customer user identifier.</param>
        /// <returns></returns>
        public async Task<CustomerUserDto> GetCustomerUserByCustomerUserId(
            int customerId, 
            string customerUserId
        )
        {
            var customerUser = await customerUsersService.GetCustomerUserByCustomerUserId(customerId, customerUserId);

            var result = Mapper.Map<CustomerUser, CustomerUserDto>(customerUser);

            return result;
        }

        /// <summary>
        /// Gets the customer user by npi.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="npi">The npi.</param>
        /// <returns></returns>
        public async Task<CustomerUserDto> GetCustomerUserByNpi(
            int customerId,
            string npi
        )
        {
            var customerUser = await customerUsersService.GetCustomerUserByNpi(customerId, npi);

            var result = Mapper.Map<CustomerUser, CustomerUserDto>(customerUser);

            return result;
        }

        /// <summary>
        /// Determines whether [is customer exists] [the specified customer identifier].
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<bool> IsCustomerExists(int customerId, string bearerToken)
        {
            var customer = await customersService.GetCustomer(customerId, bearerToken);

            return customer != null;
        }

        /// <summary>
        /// Determines whether [is customer role exists within customer] [the specified customer role identifier].
        /// </summary>
        /// <param name="customerUserRoleId">The customer role identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public async Task<bool> IsCustomerUserRoleExistsWithinCustomer(Guid customerUserRoleId, int customerId)
        {
            var customerUserRoles = await customerUsersService.GetCustomerUserRolesForCustomer(customerId);

            return customerUserRoles.Any(cr => cr.Id == customerUserRoleId);
        }

        /// <summary>
        /// Determines whether [is sites exist within customer] [the specified sites].
        /// </summary>
        /// <param name="sitesIds">The sites ids.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<bool> IsSitesExistWithinCustomer(
            IList<Guid> sitesIds,
            int customerId, 
            string bearerToken
        )
        {
            var customer = await customersService.GetCustomer(customerId, bearerToken);
            var customerSitesIds = customer.Sites.Select(s => s.Id);

            return customerSitesIds.Contains(sitesIds.ToArray());
        }

        /// <summary>
        /// Requests the email invitation.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public async Task RequestEmailInvitation(int customerId, string email)
        {
            var customerUser = await customerUsersService.GetCustomerUserByEmail(email);
            var customer = await customersService.GetCustomer(customerId, HttpContext.Current.Request.GetAuthorizationToken());

            if (customerUser != null && customerUser.CustomerId == customerId)
            {
                await emailManager.SendActivationEmail(customerUser, customer.PasswordExpirationDays);
            }
        }

        /// <summary>
        /// Requests the password reset.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public async Task RequestPasswordReset(int customerId, string email)
        {
            var customerUser = await customerUsersService.GetCustomerUserByEmail(email);
            var customer = await customersService.GetCustomer(customerId, HttpContext.Current.Request.GetAuthorizationToken());

            if (customerUser != null && customerUser.CustomerId == customerId)
            {
                await emailManager.SendResetPasswordEmail(customerUser, customer.PasswordExpirationDays);
            }
        }

        /// <summary>
        /// Gets the customer user by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        public async Task<CustomerUserDto> GetCustomerUserByEmail(
            string email,
            bool includeDeleted = false
        )
        {
            var customerUser = await customerUsersService.GetCustomerUserByEmail(email, includeDeleted);

            var result = Mapper.Map<CustomerUser, CustomerUserDto>(customerUser);

            return result;
        }

        /// <summary>
        /// Verifies if user with specified id assigned to current customer.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsCustomerUser(Guid userId)
        {
            var customerUserRole = await this.customerUsersService.GetCustomerUserPermissions(userId);

            return customerUserRole != null && customerUserRole.CustomerId == CustomerContext.Current.Customer.Id;
        }
    }
}