using LightInject;
using VitalsService.DataAccess.Document;
using VitalsService.DataAccess.EF;
using VitalsService.DomainLogic.Mappings;
using VitalsService.DomainLogic.Services.Implementations;
using VitalsService.DomainLogic.Services.Interfaces;

namespace VitalsService.DomainLogic
{
    /// <summary>
    /// DomainLogicCompositionRoot.
    /// </summary>
    public class DomainLogicCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Initializes implementations for domain logic layer.
        /// </summary>
        /// <param name="serviceRegistry"></param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterFrom<EfCompositionRoot>();
            serviceRegistry.RegisterFrom<DocumentCompositionRoot>();

            serviceRegistry.Register<IMeasurementsService, MeasurmentsService>();
            serviceRegistry.Register<ITokenService, TokenService>();
            serviceRegistry.Register<IEsb, Esb>();
            serviceRegistry.Register<IThresholdsService, ThresholdsService>();
            serviceRegistry.Register<IDefaultThresholdsService, DefaultThresholdsService>();
            serviceRegistry.Register<IHealthSessionsService, HealthSessionsService>();
            serviceRegistry.Register<IAlertsService, AlertsService>();
            serviceRegistry.Register<IAlertSeveritiesService, AlertSeveritiesService>();
            serviceRegistry.Register<IThresholdAggregator, ThresholdAggregator>();
            serviceRegistry.Register<IPatientNotesService, PatientNotesService>();
            serviceRegistry.Register<IAssessmentMediaService, AssessmentMediaService>();
            serviceRegistry.Register<IMessagingHubService, MessagingHubService>();
            serviceRegistry.Register<ITagService, TagService>();
            serviceRegistry.Register<IConditionsService, ConditionsService>();
            serviceRegistry.Register<IPatientConditionsService, PatientConditionsService>();

            // Automapper mappings
            serviceRegistry.Register<ThresholdMapping>();
            serviceRegistry.Register<PatientNotesMapping>();
        }
    }
}