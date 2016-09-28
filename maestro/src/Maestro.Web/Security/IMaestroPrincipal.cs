using System.Collections.Generic;
using System.Security.Principal;
using Maestro.Domain.Enums;

namespace Maestro.Web.Security
{
    using System;

    public interface IMaestroPrincipal : IPrincipal
    {
        Guid Id { get; set; }

        /// <summary>
        /// Returns subdomain of customer to which current user assigned.
        /// </summary>
        string CustomerSubdomain { get; }

        /// <summary>
        /// Contains list of accessible sites ids.
        /// </summary>
        IEnumerable<Guid> Sites { get; }

        /// <summary>
        /// Determines if authenticated user has one of specified roles.
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        bool IsInRoles(params string[] roles);

        /// <summary>
        /// Determines if authenticated user has one of specified customer roles.
        /// </summary>
        /// <param name="customerRoles"></param>
        /// <returns></returns>
        bool IsInCustomerRoles(params string[] customerRoles);
        
        /// <summary>
        /// Determines if authenticated user has access to specified functionality in customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        bool HasPermissions(int customerId, params CustomerUserRolePermissions[] permissions);
    }
}