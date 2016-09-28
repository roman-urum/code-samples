using System;
using System.Collections.Generic;
using System.Web;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos.TokenService.RequestsResponses;

namespace Maestro.Web.Security
{
    /// <summary>
    /// Declares methods to authenticate users in Maestro website.
    /// </summary>
    public interface IAuthenticator
    {
        /// <summary>
        /// Returns time when authentication session expires.
        /// </summary>
        DateTime GetSessionExpirationTime();

        /// <summary>
        /// Saves user auth data and decorates request principal.
        /// </summary>
        /// <param name="user">User account info.</param>
        /// <param name="tokenResponse">Response with API authentication token.</param>
        /// <param name="role">Role of authenticated user.</param>
        /// <param name="sessionTimeout"></param>
        /// <param name="permissions">Detailed permissions of role (if required).</param>
        /// <param name="sites">List of sites ids available for user.</param>
        void StartAuthenticationSession(
            User user,
            TokenResponseModel tokenResponse,
            string role,
            TimeSpan sessionTimeout,
            PermissionsAuthData permissions = null,
            IEnumerable<Guid> sites = null
        );

        /// <summary>
        /// Extends session lifetime.
        /// </summary>
        void ExtendSessionLifetime();

        /// <summary>
        /// Removes authentication data from session.
        /// </summary>
        void ClearAuthenticationSession();

        /// <summary>
        /// Decorates default principal with custom Maestro principal.
        /// </summary>
        /// <param name="context"></param>
        void DecorateRequestPrincipal(HttpContext context);
    }
}
