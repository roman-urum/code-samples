using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Maestro.Web.Extensions;

namespace Maestro.Web.Controls
{
    /// <summary>
    /// Contains extension methods to generate action link
    /// with customer url.
    /// </summary>
    public static class CustomerActionLinkControl
    {
        /// <summary>
        /// Returns html string for link to customer website.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText"></param>
        /// <param name="subdomain"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static MvcHtmlString CustomerActionLink(this HtmlHelper htmlHelper, string linkText, string subdomain,
            string action, string controller, object routeValues = null)
        {
            if (string.IsNullOrEmpty(subdomain))
            {
                // TODO: Argument null exception should be placed below.
                return new MvcHtmlString(string.Empty);
            }

            var context = new HttpContextWrapper(HttpContext.Current);
            var protocol = context.Request.GetProtocol();
            var host = string.Concat(subdomain, WebSettings.Domain);

            return htmlHelper.ActionLink(linkText, action, controller, protocol, host, null, routeValues, null);
        }
    }
}