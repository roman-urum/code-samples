using AutoMapper;
using DeviceService.DomainLogic.Mappings;

namespace DeviceService.DomainLogic
{
    /// <summary>
    /// Contains declaration of all mappings used in Domain Logic.
    /// </summary>
    public static class DomainLogicAutomapperConfig
    {
        public static void RegisterRules(IConfiguration config)
        {
            config.AddProfile<DevicesMapping>();
        }
    }
}
