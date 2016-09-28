using AutoMapper;
using VitalsService.DomainLogic;
using VitalsService.Web.Api.Models.Mappings;

namespace VitalsService.Web.Api
{
    /// <summary>
    /// Configuration of objects properties mapping.
    /// </summary>
    public class AutomapperConfig
    {
        /// <summary>
        /// Registers the mapping rules.
        /// </summary>
        public static void RegisterRules()
        {
            Mapper.Initialize(config =>
            {
                DomainLogicAutomapperConfig.RegisterRules(config);

                config.AddProfile<AlertMapping>();
                config.AddProfile<AlertSeverityMapping>();
                config.AddProfile<AssessmentMediaMapping>();
                config.AddProfile<CommonMapping>();
                config.AddProfile<HealthSessionMapping>();
                config.AddProfile<PatientNotesMapping>();
                config.AddProfile<ThresholdMapping>();
                config.AddProfile<VitalsMapping>();
                config.AddProfile<ConditionsMapping>();
            });
        }
    }
}