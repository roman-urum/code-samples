using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maestro.Domain.Dtos.CustomerService;
using Maestro.Web.Exceptions;

namespace Maestro.Web
{
    /// <summary>
    /// Container for instance of current site context.
    /// </summary>
    public static class SiteContext
    {
        public const string SiteAreaName = "Site";

        /// <summary>
        /// Returns site context for current site.
        /// </summary>
        public static ISiteContext Current
        {
            get { return DependencyResolver.Current.GetService<ISiteContext>(); }
        }
    }

    /// <summary>
    /// Provides info about current site with which
    /// user working at this time.
    /// </summary>
    public interface ISiteContext
    {
        /// <summary>
        /// Returns site with which current user works.
        /// </summary>
        SiteResponseDto Site { get; }
    }

    /// <summary>
    /// Default implementation of context for current site.
    /// </summary>
    public class DefaultSiteContext : ISiteContext
    {
        private const string SiteIdRouteName = "siteId";
        private const string SiteIdSessionKey = "siteId";

        /// <summary>
        /// Returns site with which current user works.
        /// </summary>
        public SiteResponseDto Site { get; private set; }

        public DefaultSiteContext()
        {
            var customer = CustomerContext.Current.Customer;

            if (customer == null)
            {
                return;
            }

            var siteId = GetSiteId();

            if (!siteId.HasValue)
            {
                return;
            }

            Site = customer.Sites.FirstOrDefault(s => s.Id == siteId.Value);
            SaveSiteId(siteId.Value);
        }

        /// <summary>
        /// Returns site id value specified in routes.
        /// Returns null if value not specified.
        /// </summary>
        /// <returns></returns>
        private static Guid? GetSiteId()
        {
            var routeValue = HttpContext.Current.Request.RequestContext.RouteData.Values[SiteIdRouteName];
            Guid siteId;
            
            if (routeValue != null && Guid.TryParse(routeValue.ToString(), out siteId))
            {
                return siteId;
            }
            
            var sessionValue = HttpContext.Current.Session[SiteIdSessionKey];

            if (sessionValue != null && Guid.TryParse(sessionValue.ToString(), out siteId))
            {
                return siteId;
            }

            return null;
        }

        /// <summary>
        /// Saves site id in session.
        /// </summary>
        /// <param name="siteId"></param>
        private static void SaveSiteId(Guid siteId)
        {
            HttpContext.Current.Session[SiteIdSessionKey] = siteId;
        }
    }
}