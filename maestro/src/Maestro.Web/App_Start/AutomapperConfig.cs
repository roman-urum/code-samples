using AutoMapper;
using Maestro.DomainLogic.Mappings;
using Maestro.Web.Models.Mappings;

namespace Maestro.Web
{
    /// <summary>
    /// Provides methods to initialize mapping rules.
    /// </summary>
    public static class AutomapperConfig
    {
        /// <summary>
        /// Initializes mapping rules.
        /// </summary>
        public static void RegisterRules()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<DomainLogicMappings>();

                config.AddProfile<CustomerMappings>();
                config.AddProfile<UserMappings>();
                config.AddProfile<CareBuilderMappings>();
                config.AddProfile<PatientMappings>();
            });
        }
    }
}