using System.Web.Http;
using CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic;
using LightInject;
using LightInject.ServiceLocation;
using Microsoft.Practices.ServiceLocation;

namespace CareInnovations.HealthHarmony.Maestro.TokenService
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
            serviceContainer.EnablePerWebRequestScope();
            serviceContainer.EnableWebApi(GlobalConfiguration.Configuration);

            serviceContainer.RegisterFrom<WebApiCompositionRoot>();
            serviceContainer.RegisterFrom<DomainLogicCompositionRoot>();

            ServiceLocator.SetLocatorProvider(() => new LightInjectServiceLocator(serviceContainer));
        }
    }
}