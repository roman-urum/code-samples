using System.Web.Http;
using Isg.EntityFramework.Interceptors;
using Microsoft.Practices.ServiceLocation;

namespace HealthLibrary.Web.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(SwaggerConfig.Register);
            IocConfig.RegisterDependencies();
            AutomapperConfig.RegisterRules();
            InterceptorProvider.SetInterceptorProvider(ServiceLocator.Current.GetInstance<IInterceptorProvider>());
        }
    }
}