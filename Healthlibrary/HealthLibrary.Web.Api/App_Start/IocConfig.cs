using System.Web.Http;
using HealthLibrary.DomainLogic;
using LightInject;
using LightInject.ServiceLocation;
using Microsoft.Practices.ServiceLocation;

namespace HealthLibrary.Web.Api
{
    /// <summary>
    /// IocConfig.
    /// </summary>
    public class IocConfig
    {
        /// <summary>
        /// Registers the dependencies.
        /// </summary>
        public static void RegisterDependencies()
        {
            var serviceContainer = new ServiceContainer();

            serviceContainer.RegisterApiControllers();
            serviceContainer.EnableWebApi(GlobalConfiguration.Configuration);
            serviceContainer.EnablePerWebRequestScope();

            serviceContainer.RegisterFrom<DomainLogicCompositionRoot>();
            serviceContainer.RegisterFrom<WebApiCompositionRoot>();

            ServiceLocator.SetLocatorProvider(() => new LightInjectServiceLocator(serviceContainer));
        }
    }
}