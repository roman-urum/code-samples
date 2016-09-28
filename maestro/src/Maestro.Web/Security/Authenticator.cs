using System;
using System.Collections.Generic;
using System.Web;
using Maestro.Common;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos.TokenService.RequestsResponses;

namespace Maestro.Web.Security
{
    /// <summary>
    /// Provides methods to authorize users.
    /// </summary>
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthDataStorage authDataStorage;

        public Authenticator(IAuthDataStorage authDataStorage)
        {
            this.authDataStorage = authDataStorage;
        }

        /// <summary>
        /// Returns time when authentication session expires.
        /// </summary>
        public DateTime GetSessionExpirationTime()
        {
            return DateTime.Now.Add(Settings.DefaultSessionTimeout);
        }

        /// <summary>
        /// Saves user auth data and decorates request principal.
        /// </summary>
        /// <param name="user">User account info.</param>
        /// <param name="tokenResponse">Response with API authentication token.</param>
        /// <param name="role">Role of authenticated user.</param>
        /// <param name="sessionTimeout"></param>
        /// <param name="permissions">Detailed permissions of role (if required).</param>
        /// <param name="sites">List of sites ids available for user.</param>
        public void StartAuthenticationSession(
            User user,
            TokenResponseModel tokenResponse,
            string role,
            TimeSpan sessionTimeout,
            PermissionsAuthData permissions = null,
            IEnumerable<Guid> sites = null
            )
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(tokenResponse.Id))
            {
                throw new ArgumentNullException("authToken");
            }

            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentNullException("role");
            }

            var userAuthData = new UserAuthData()
            {
                UserId = user.Id,
                CustomerId = user is CustomerUser ? ((CustomerUser) user).CustomerId : Settings.CICustomerId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenResponse.Id,
                PasswordExpirationUtc = tokenResponse.ExpirationUtc,
                Role = role,
                CustomerRole = user is CustomerUser ? ((CustomerUser)user).CustomerUserRole.Name : string.Empty,
                Permissions = permissions,
                Sites = sites,
                SessionTimeout = sessionTimeout,
                Expires = DateTime.Now + sessionTimeout
            };

            authDataStorage.Save(userAuthData);

            HttpContext.Current.User = new MaestroPrincipal(userAuthData);
        }

        /// <summary>
        /// Removes authentication cookies.
        /// </summary>
        public void ClearAuthenticationSession()
        {
            authDataStorage.Clear();
        }

        /// <summary>
        /// Decorates default principal with custom Maestro principal.
        /// </summary>
        /// <param name="context"></param>
        public void DecorateRequestPrincipal(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (IsDecorated(context))
            {
                return;
            }

            var authData = authDataStorage.GetUserAuthData();
            context.User = new MaestroPrincipal(authData);
        }

        /// <summary>
        /// Extends session lifetime.
        /// </summary>
        public void ExtendSessionLifetime()
        {
            var authData = this.authDataStorage.GetUserAuthData();

            if (authData == null)
            {
                return;
            }

            authData.Expires = DateTime.Now + authData.SessionTimeout;
            this.authDataStorage.Save(authData);

            return;
        }

        #region Private methods

        /// <summary>
        /// Checks if request cannot be decorated.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static bool IsDecorated(HttpContext context)
        {
            return context.User is MaestroPrincipal;
        }

        #endregion
    }
}
