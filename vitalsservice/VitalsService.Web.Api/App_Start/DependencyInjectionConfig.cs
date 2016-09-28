using System.Web.Http;
using LightInject;
using LightInject.ServiceLocation;
using Microsoft.Practices.ServiceLocation;
using VitalsService.ContentStorage.Azure;
using VitalsService.DomainLogic;

namespace VitalsService.Web.Api
{
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Initializes all dependensies in solution.
        /// </summary>
        public static void RegistedDependencies()
        {
            var container = new ServiceContainer();

            container.RegisterApiControllers();
            container.RegisterFrom<ContentStorageCompositionRoot>();
            container.RegisterFrom<DomainLogicCompositionRoot>();
            container.RegisterFrom<WebApiCompositionRoot>();

            container.EnableWebApi(GlobalConfiguration.Configuration);
            container.EnablePerWebRequestScope();

            ServiceLocator.SetLocatorProvider(() => new LightInjectServiceLocator(container));
        }
    }
}