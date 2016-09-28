using System.Web;
using System.Web.SessionState;

namespace Maestro.Web
{
    /// <summary>
    /// Custom session manager to initialize session cookies
    /// with required domain using first level domain specified in request.
    /// </summary>
    public class MaestroSessionIdManager : SessionIDManager, ISessionIDManager
    {
        private const string SessionIdCookieName = "ASP.NET_SessionId";

        /// <summary>
        /// Executes default implementation from SessionIDManager and updates
        /// cookie domain with first level domain specified in request.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="redirected"></param>
        /// <param name="cookieAdded"></param>
        void ISessionIDManager.SaveSessionID(HttpContext context, string id, out bool redirected, out bool cookieAdded)
        {
            base.SaveSessionID(context, id, out redirected, out cookieAdded);

            if (cookieAdded)
            {
                var cookie = context.Response.Cookies[SessionIdCookieName];

                cookie.Domain = WebSettings.Domain;
            }
        }
    }
}