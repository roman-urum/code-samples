using AutoMapper;
using CustomerService.DomainLogic.Mappings;

namespace CustomerService.DomainLogic
{
    /// <summary>
    /// Contains declaration of all mappings used in Domain Logic.
    /// </summary>
    public static class DomainLogicAutomapperConfig
    {
        /// <summary>
        /// Registers the rules.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void RegisterRules(IConfiguration config)
        {
            config.AddProfile<DomainLogicMapping>();
        }
    }
}