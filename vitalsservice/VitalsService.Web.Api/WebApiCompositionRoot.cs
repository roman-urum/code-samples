using System.Configuration;
using LightInject;
using StackExchange.Redis;
using VitalsService.Web.Api.Helpers;
using VitalsService.Web.Api.Helpers.Implementation;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models.Mappings;
using VitalsService.Web.Api.Security;
using WebApi.OutputCache.Core.Cache;

namespace VitalsService.Web.Api
{
    /// <summary>
    /// WebApiCompositionRoot.
    /// </summary>
    public class WebApiCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Composes the specified service registry.
        /// </summary>
        /// <param name="serviceRegistry">The service registry.</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            #region Helpers
            
            serviceRegistry.Register<IThresholdsControllerHelper, ThresholdsControllerHelper>();
            serviceRegistry.Register<IDefaultThresholdsControllerHelper, DefaultThresholdsControllerHelper>();
            serviceRegistry.Register<IHealthSessionsControllerHelper, HealthSessionsControllerHelper>();
            serviceRegistry.Register<IVitalsControllerHelper, VitalsControllerHelper>();
            serviceRegistry.Register<IAlertsControllerHelper, AlertsControllerHelper>();
            serviceRegistry.Register<IAlertSeveritiesControllerHelper, AlertSeveritiesControllerHelper>();
            serviceRegistry.Register<ISuggestedNotablesControllerHelper, SuggestedNotablesControllerHelper>();
            serviceRegistry.Register<INotesControllerHelper, NotesControllerHelper>();
            serviceRegistry.Register<IAssessmentMediaControllerHelper, AssessmentMediaControllerHelper>();
            serviceRegistry.Register<IConditionsControllerHelper, ConditionsControllerHelper>();
            serviceRegistry.Register<IPatientConditionsControllerHelper, PatientConditionsControllerHelper>();

            #endregion

            #region Contexts

            serviceRegistry.Register<ICustomerContext, CustomerContext>(new PerScopeLifetime());
            serviceRegistry.Register<IUserContext, UserContext>(new PerScopeLifetime());
            serviceRegistry.Register<ICertificateAuthContext, DefaultCertificateAuthContext>(new PerScopeLifetime());

            #endregion

            #region Redis cache

            serviceRegistry.Register<ConnectionMultiplexer>(factory =>
                ConnectionMultiplexer.Connect(
                    ConfigurationManager.ConnectionStrings["RedisCache"].ConnectionString, null),
                new PerContainerLifetime());

            serviceRegistry.Register<IDatabase>(factory =>
                factory.GetInstance<ConnectionMultiplexer>().GetDatabase(-1, null));

            serviceRegistry.Register<IApiOutputCache, StackExchangeRedisOutputCacheProvider>();

            #endregion

            // Automapper mappings
            serviceRegistry.Register<HealthSessionMapping>();
            serviceRegistry.Register<ThresholdMapping>();
            serviceRegistry.Register<VitalsMapping>();
            serviceRegistry.Register<CommonMapping>();
            serviceRegistry.Register<AlertMapping>();
            serviceRegistry.Register<AlertSeverityMapping>();
            serviceRegistry.Register<PatientNotesMapping>();
            serviceRegistry.Register<AssessmentMediaMapping>();
            serviceRegistry.Register<ConditionsMapping>();
        }
    }
}