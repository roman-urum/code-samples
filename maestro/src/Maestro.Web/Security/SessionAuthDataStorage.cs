using System;
using System.Web;
using NLog;

namespace Maestro.Web.Security
{
    /// <summary>
    /// Allows to store auth token in session.
    /// </summary>
    public class SessionAuthDataStorage : IAuthDataStorage
    {
        private const string SessionKey = "AuthData";

        /// <summary>
        /// Saves API authentication token and user data.
        /// </summary>
        /// <param name="authData"></param>
        public void Save(UserAuthData authData)
        {
            HttpContext.Current.Session[SessionKey] = authData;
        }

        /// <summary>
        /// Returns saved authentication info.
        /// </summary>
        public UserAuthData GetUserAuthData()
        {
            object sessionValue = HttpContext.Current.Session[SessionKey];

            var authData = sessionValue as UserAuthData;

            if (authData == null)
            {
                return null;
            }

            if (authData.Expires < DateTime.Now)
            {
                this.Clear();

                return null;
            }

            return authData;
        }

        /// <summary>
        /// Returns authentication token or null 
        /// if token not saved in session.
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            var authData = this.GetUserAuthData();

            return authData == null ? null : authData.Token;
        }

        /// <summary>
        /// Removes auth token session.
        /// </summary>
        public void Clear()
        {
            HttpContext.Current.Session.Remove(SessionKey);
        }
    }
}