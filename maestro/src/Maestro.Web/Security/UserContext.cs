using System;
using Microsoft.Practices.ServiceLocation;

namespace Maestro.Web.Security
{
    /// <summary>
    /// Static container to access user auth data.
    /// </summary>
    public static class UserContext
    {
        /// <summary>
        /// Timeout to send resume session request in UI.
        /// </summary>
        public static double ResumeSessionTimeout
        {
            get
            {
                var authData = ServiceLocator.Current.GetInstance<IAuthDataStorage>().GetUserAuthData();

                return authData.SessionTimeout.TotalMilliseconds - WebSettings.TimeToHandleRequest;
            }
        }
    }
}