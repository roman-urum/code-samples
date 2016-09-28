using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos;

namespace Maestro.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// Service to manage relationships between customers and users.
    /// </summary>
    public interface ICustomerUsersService
    {
        /// <summary>
        /// Creates the customer user.
        /// </summary>
        /// <param name="customerUser">The customer user.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<CreateCustomerUserResultDto> CreateCustomerUser(CustomerUser customerUser, string bearerToken);

        /// <summary>
        /// Deletes the customer user.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerUserId">The customer user identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteCustomerUser(int customerId, Guid customerUserId);

        /// <summary>
        /// Saves info about sites to which user has access.
        /// </summary>
        /// <param name="customerUserId"></param>
        /// <param name="sitesIds"></param>
        /// <returns></returns>
        Task SetSites(Guid customerUserId, IList<Guid> sitesIds);

        /// <summary>
        /// Returns list of sites available for specified user.
        /// </summary>
        /// <param name="customerUserId"></param>
        /// <returns></returns>
        Task<IEnumerable<CustomerUserSite>> GetCustomerUserSites(Guid customerUserId);

        /// <summary>
        /// Assign customer role to customer user
        /// </summary>
        /// <param name="customerUserId">The user identifier.</param>
        /// <param name="customerUserRoleId">The customer role identifier.</param>
        /// <returns></returns>
        Task SetCustomerUserRole(Guid customerUserId, Guid customerUserRoleId);

        /// <summary>
        /// Returns customer role assigned to user with list 
        /// of permissions included.
        /// </summary>
        /// <param name="customerUserId"></param>
        /// <returns></returns>
        Task<CustomerUserRole> GetCustomerUserPermissions(Guid customerUserId);

        /// <summary>
        /// Gets the customer users.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Task<IList<CustomerUser>> GetCustomerUsers(int customerId);

        /// <summary>
        /// Gets the customer user by customer user identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerUserId">The customer user identifier.</param>
        /// <returns></returns>
        Task<CustomerUser> GetCustomerUserByCustomerUserId(int customerId, string customerUserId);

        /// <summary>
        /// Gets the customer user by npi.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="npi">The npi.</param>
        /// <returns></returns>
        Task<CustomerUser> GetCustomerUserByNpi(int customerId, string npi);

        /// <summary>
        /// Updates the customer user.
        /// </summary>
        /// <param name="customerUser">The customer user.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<UpdateCustomerUserResultDto> UpdateCustomerUser(CustomerUser customerUser, string bearerToken);

        /// <summary>
        /// Gets the customer user roles for customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Task<IList<CustomerUserRole>> GetCustomerUserRolesForCustomer(int customerId);

        /// <summary>
        /// Gets the customer user by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        Task<CustomerUser> GetCustomerUserByEmail(string email, bool includeDeleted = false);

        /// <summary>
        /// Gets the care managers.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId"></param>
        /// <returns></returns>
        Task<IList<CustomerUser>> GetCareManagers(int customerId, Guid siteId);
    }
}
