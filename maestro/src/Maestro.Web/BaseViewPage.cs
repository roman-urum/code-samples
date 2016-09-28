using System.Web;
using System.Web.Mvc;
using Maestro.Web.Helpers;
using Maestro.Web.Security;

namespace Maestro.Web
{
    /// <summary>
    /// BaseViewPage.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.WebViewPage" />
    public abstract class BaseViewPage : WebViewPage
    {
        public readonly CssHelper Css = new CssHelper(HttpContext.Current);
        public readonly PagePermissionsHelper PagePermissions =
            new PagePermissionsHelper(HttpContext.Current.User as IMaestroPrincipal);

        public virtual new IMaestroPrincipal User
        {
            get { return base.User as IMaestroPrincipal; }
        }
    }

    /// <summary>
    /// BaseViewPage.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="System.Web.Mvc.WebViewPage" />
    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public readonly CssHelper Css = new CssHelper(HttpContext.Current);
        public readonly PagePermissionsHelper PagePermissions =
            new PagePermissionsHelper(HttpContext.Current.User as IMaestroPrincipal);

        public virtual new IMaestroPrincipal User
        {
            get { return base.User as IMaestroPrincipal; }
        }
    }
}