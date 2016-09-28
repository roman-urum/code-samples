using AutoMapper;
using VitalsService.DomainLogic.Mappings;

namespace VitalsService.DomainLogic
{
    /// <summary>
    /// Contains declaration of all mappings used in Domain Logic.
    /// </summary>
    public static class DomainLogicAutomapperConfig
    {
        /// <summary>
        /// Initializes mappings with custom config.
        /// </summary>
        public static void RegisterRules()
        {
            Mapper.Initialize(RegisterRules);
        }

        /// <summary>
        /// Initializes mappings in provided config.
        /// </summary>
        /// <param name="config"></param>
        public static void RegisterRules(IConfiguration config)
        {
            config.AddProfile<PatientNotesMapping>();
            config.AddProfile<ThresholdMapping>();
            config.AddProfile<ConditionsMapping>();
        }
    }
}