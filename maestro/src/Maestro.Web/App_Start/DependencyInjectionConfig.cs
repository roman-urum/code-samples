using System.Web.Http;
using LightInject;
using LightInject.ServiceLocation;
using Maestro.DataAccess.Api;
using Maestro.DataAccess.EF;
using Maestro.DataAccess.Redis;
using Maestro.DomainLogic;
using Maestro.Reporting;
using Microsoft.Practices.ServiceLocation;

namespace Maestro.Web
{
    /// <summary>
    /// DependencyInjectionConfig.
    /// </summary>
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Registers the dependencies.
        /// </summary>
        public static void RegisterDependencies()
        {
            var diContainer = new ServiceContainer();
            diContainer.RegisterControllers();
            diContainer.EnableMvc();

            diContainer.RegisterApiControllers();
            diContainer.EnableWebApi(GlobalConfiguration.Configuration);
            diContainer.EnablePerWebRequestScope();

            diContainer.RegisterFrom<DataAccessCompositionRoot>();
            diContainer.RegisterFrom<DataAccessEfCompositionRoot>();
            diContainer.RegisterFrom<DataAccessRedisCompositionRoot>();
            diContainer.RegisterFrom<DomainLogicCompositionRoot>();
            diContainer.RegisterFrom<ReportingCompositionRoot>();
            diContainer.RegisterFrom<WebCompositionRoot>();

            ServiceLocator.SetLocatorProvider(() => new LightInjectServiceLocator(diContainer));
        }
    }
}