using System.Web.Http;
using Isg.EntityFramework.Interceptors;
using Microsoft.Practices.ServiceLocation;

namespace CareInnovations.HealthHarmony.Maestro.TokenService
{
    /// <summary>
    /// WebApiApplication.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Application_s the start.
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            IocConfig.RegisterDependencies();
            AutoMapperConfig.RegisterProfiles();
            InterceptorProvider.SetInterceptorProvider(ServiceLocator.Current.GetInstance<IInterceptorProvider>());
        }
    }
}