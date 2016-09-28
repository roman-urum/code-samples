using System;
using System.Linq;
using System.Web;
using Maestro.Web.Extensions;

namespace Maestro.Web.Helpers
{
    /// <summary>
    /// Helper to generate styles on pages.
    /// </summary>
    public class CssHelper
    {
        private const string ActiveTabClass = "active";

        private readonly HttpContext httpContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CssHelper"/> class.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        public CssHelper(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }

        /// <summary>
        /// Generates styles for active tab by particular controller and it's actions.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="actions">The actions.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public string ActiveTab(string controller, string[] actions)
        {
            if (string.IsNullOrEmpty(controller))
            {
                throw new ArgumentNullException("controller");
            }

            var currentController = httpContext.Request.RequestContext.RouteData.GetController();
            var currentAction = httpContext.Request.RequestContext.RouteData.GetAction();

            if (!actions.Any())
            {
                return controller.Equals(currentController) ? ActiveTabClass : string.Empty;
            }

            return controller.Equals(currentController) && actions.Any(a => a.Equals(currentAction)) ?
                ActiveTabClass : 
                string.Empty;
        }
    }
}