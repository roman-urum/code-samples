using System;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Isg.EntityFramework.Interceptors;
using Maestro.Common.Exceptions;
using Maestro.Web.Areas.Customer;
using Maestro.Web.Binders;
using Maestro.Web.Controllers;
using Maestro.Web.Exceptions;
using Maestro.Web.Security;
using Microsoft.Practices.ServiceLocation;
using NLog;

namespace Maestro.Web
{
    public class MvcApplication : HttpApplication
    {
        private static readonly Logger eventLogger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyInjectionConfig.RegisterDependencies();
            InterceptorProvider.SetInterceptorProvider(ServiceLocator.Current.GetInstance<IInterceptorProvider>());
            AutomapperConfig.RegisterRules();
            PagesPermissionsConfig.Initialize();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomerViewEngine());

            RegisterModelBinders();
        }

        /// <summary>
        /// Removes auth token when session initialized if user not authenticated.
        /// </summary>
        protected void Application_AcquireRequestState()
        {
            if (HttpContext.Current.Session == null)
            {
                return;
            }

            var authenticator = DependencyResolver.Current.GetService<IAuthenticator>();

            authenticator.ExtendSessionLifetime();
            authenticator.DecorateRequestPrincipal(HttpContext.Current);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var httpContext = ((MvcApplication)sender).Context;

            var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));
            var currentController = string.Empty;
            var currentAction = string.Empty;

            if (currentRouteData != null)
            {
                if (currentRouteData.Values["controller"] != null &&
                    !string.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
                {
                    currentController = currentRouteData.Values["controller"].ToString();
                }

                if (currentRouteData.Values["action"] != null &&
                    !string.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
                {
                    currentAction = currentRouteData.Values["action"].ToString();
                }
            }

            var ex = Server.GetLastError();

            eventLogger.Error(ex, "Unhandled exception occured.");

            var controller = new ErrorsController();
            var routeData = new RouteData();
            var action = "Index";

            if (ex is HttpException)
            {
                var statusCode = ((HttpException)ex).GetHttpCode();

                httpContext.Response.StatusCode = statusCode;

                switch (statusCode)
                {
                    case (int)HttpStatusCode.NotFound:
                        action = "NotFoundError";
                        break;

                    default:
                        action = "Index";
                        break;
                }
            }
            else if (ex is LogicException)
            {
                action = "ErrorMessage";
                routeData.Values["message"] = ex.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (ex is ServiceException)
            {
                action = "ErrorMessage";
                routeData.Values["message"] = ex.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            else
            {
                action = "ErrorMessage";
                routeData.Values["message"] = string.Format("{0} -> {1}", ex.Message, ex.StackTrace);                
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            httpContext.ClearError();
            httpContext.Response.Clear();
            
            httpContext.Response.TrySkipIisCustomErrors = true;
            routeData.Values["controller"] = "Errors";
            routeData.Values["action"] = action;
            routeData.Values["area"] = string.Empty;

            controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
            ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
        }

        private void RegisterModelBinders()
        {
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
        }
    }
}