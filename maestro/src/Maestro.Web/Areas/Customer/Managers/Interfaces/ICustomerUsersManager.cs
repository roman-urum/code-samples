using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Web.Areas.Customer.Models.Settings.CustomerUsers;
using Maestro.Web.Models.Api.Dtos.Entities;
using Maestro.Web.Models.Api.Dtos.RequestsResponses;

namespace Maestro.Web.Areas.Customer.Managers.Interfaces
{
    /// <summary>
    /// Service to manage users in customer area.
    /// </summary>
    public interface ICustomerUsersManager
    {
        /// <summary>
        /// Returns list of users assigned to specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<List<CustomerUserListViewModel>> GetCustomerUsers(int customerId);

        /// <summary>
        /// Returns list of user roles for current customer.
        /// </summary>
        /// <returns></returns>
        Task<IList<CustomerUserRoleViewModel>> GetCustomerUserRoles();

        /// <summary>
        /// Saves info about customer user in database and token service.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<CreateCustomerUserResultDto> CreateCustomerUser(CustomerUserViewModel model);

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<CreateCustomerUserResultDto> CreateCustomerUser(int customerId, CreateCustomerUserRequestDto request);

        /// <summary>
        /// Updates customer user data.
        /// </summary>
        /// <returns></returns>
        Task<UpdateCustomerUserResultDto> UpdateCustomerUser(Guid userId, CustomerUserViewModel model);

        /// <summary>
        /// Updates the customer user.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerUserId">The customer user identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<UpdateCustomerUserResultDto> UpdateCustomerUser(int customerId, Guid customerUserId, UpdateCustomerUserRequestDto request);

        /// <summary>
        /// Deletes the customer user.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerUserId">The customer user identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteCustomerUser(int customerId, Guid customerUserId);

        /// <summary>
        /// Updates isenabled field for user with specified id
        /// (only for customer users).
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        Task SetEnabledState(Guid userId, bool isEnabled);

        /// <summary>
        /// Determines whether customer user NPI is unique within a customer user.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="npi">The npi.</param>
        /// <param name="databaseCustomerUserId">The database customer user identifier.</param>
        /// <returns></returns>
        Task<bool> IsNpiUniqueWithinCustomerUser(
            int customerId,
            string npi,
            Guid? databaseCustomerUserId = null
        );

        /// <summary>
        /// Determines whether customer user id is unique within a customer user.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerUserId">The customer user identifier.</param>
        /// <param name="databaseCustomerUserId">The database customer user identifier.</param>
        /// <returns></returns>
        Task<bool> IsCustomerUserIdUniqueWithinCustomerUser(
            int customerId,
            string customerUserId,
            Guid? databaseCustomerUserId = null
        );

        /// <summary>
        /// Gets the customer user by customer user identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerUserId">The customer user identifier.</param>
        /// <returns></returns>
        Task<CustomerUserDto> GetCustomerUserByCustomerUserId(int customerId, string customerUserId);

        /// <summary>
        /// Gets the customer user by npi.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="npi">The npi.</param>
        /// <returns></returns>
        Task<CustomerUserDto> GetCustomerUserByNpi(int customerId, string npi);

        /// <summary>
        /// Determines whether [is customer exists] [the specified customer identifier].
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<bool> IsCustomerExists(int customerId, string bearerToken);

        /// <summary>
        /// Determines whether [is customer role exists within customer] [the specified customer role identifier].
        /// </summary>
        /// <param name="customerUserRoleId">The customer role identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Task<bool> IsCustomerUserRoleExistsWithinCustomer(Guid customerUserRoleId, int customerId);

        /// <summary>
        /// Determines whether [is sites exist within customer] [the specified sites].
        /// </summary>
        /// <param name="sitesIds">The sites ids.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<bool> IsSitesExistWithinCustomer(IList<Guid> sitesIds, int customerId, string bearerToken);

        /// <summary>
        /// Requests the email invitation.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task RequestEmailInvitation(int customerId, string email);

        /// <summary>
        /// Requests the password reset.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task RequestPasswordReset(int customerId, string email);

        /// <summary>
        /// Gets the customer user by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        Task<CustomerUserDto> GetCustomerUserByEmail(string email, bool includeDeleted = false);
    }
}