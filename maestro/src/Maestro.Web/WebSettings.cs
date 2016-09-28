using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Maestro.Common.Extensions;

namespace Maestro.Web
{
    /// <summary>
    /// Provides access to Maestro web settings.
    /// </summary>
    public static class WebSettings
    {
        private const char DomainsDivider = '.';
        private const string DomainTemplate = ".{0}.{1}";

        /// <summary>
        /// Time reserved to handle request to resume session.
        /// </summary>
        public const int TimeToHandleRequest = 10000;

        /// <summary>
        /// Returns root url of customer website.
        /// </summary>
        public static string Domain
        {
            get
            {
                string value = ConfigurationManager.AppSettings["Domain"];

                return string.IsNullOrEmpty(value) ? GetDefaultDomain() : value;
            }
        }

        /// <summary>
        /// Url of login page.
        /// </summary>
        public static string LoginPage
        {
            get { return ConfigurationManager.AppSettings["LoginPage"]; }
        }

        /// <summary>
        /// Url of page to redirect if user hasn't assigned sites.
        /// </summary>
        public static string NoAccessSitesPage
        {
            get { return ConfigurationManager.AppSettings["NoAccessSitesPage"]; }
        }

        /// <summary>
        /// Returns default page path for customer site.
        /// </summary>
        public static string CustomerDefaultPage
        {
            get { return ConfigurationManager.AppSettings["CustomerDefaultPage"]; }
        }

        /// <summary>
        /// Generates default domain by request host.
        /// </summary>
        /// <returns></returns>
        private static string GetDefaultDomain()
        {
            string[] hostParts = HttpContext.Current.Request.Url.Host.Split(DomainsDivider);
            string topLevelDomain = hostParts.Last();
            string websiteDomain = hostParts[hostParts.Count() - 2];

            return DomainTemplate.FormatWith(websiteDomain, topLevelDomain);
        }
    }
}