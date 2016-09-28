using System.Data.Entity;
using System.Web.Http;
using DeviceService.Common;
using Isg.EntityFramework.Interceptors;
using LightInject;
using LightInject.ServiceLocation;
using Microsoft.Practices.ServiceLocation;

namespace DeviceService.Web.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
       
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            this.RegisterCompositionRoot();
            InterceptorProvider.SetInterceptorProvider(ServiceLocator.Current.GetInstance<IInterceptorProvider>());
            AutomapperConfig.RegisterRules(ServiceLocator.Current.GetInstance<IAppSettings>());
            FilterConfig.RegisterGlobalFilters(GlobalConfiguration.Configuration.Filters);
        }

        private void RegisterCompositionRoot()
        {
            var serviceContainer = new ServiceContainer();
            
            serviceContainer.RegisterApiControllers();
            serviceContainer.EnablePerWebRequestScope();
            serviceContainer.EnableWebApi(GlobalConfiguration.Configuration);

            serviceContainer.RegisterFrom<WebApiCompositionRoot>();            

            ServiceLocator.SetLocatorProvider(() => new LightInjectServiceLocator(serviceContainer));
        }
    }
}