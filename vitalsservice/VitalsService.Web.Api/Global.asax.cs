using System.Web.Http;
using Isg.EntityFramework.Interceptors;
using Microsoft.Practices.ServiceLocation;

namespace VitalsService.Web.Api
{
    /// <summary>
    /// WebApiApplication.
    /// </summary>
    /// <seealso cref="System.Web.HttpApplication" />
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Application_s the start.
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            DependencyInjectionConfig.RegistedDependencies();
            AutomapperConfig.RegisterRules();
            FilterConfig.RegisterGlobalFilters(GlobalConfiguration.Configuration.Filters);
            InterceptorProvider.SetInterceptorProvider(ServiceLocator.Current.GetInstance<IInterceptorProvider>());
        }
    }
}