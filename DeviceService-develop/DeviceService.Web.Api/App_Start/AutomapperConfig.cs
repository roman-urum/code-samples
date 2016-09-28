using AutoMapper;
using DeviceService.Common;
using DeviceService.DomainLogic;
using DeviceService.Web.Api.Models.Mapping;

namespace DeviceService.Web.Api
{
    /// <summary>
    /// AutomapperConfig.
    /// </summary>
    public static class AutomapperConfig
    {
        /// <summary>
        /// Registers the rules.
        /// </summary>
        public static void RegisterRules(IAppSettings appSettings)
        {
            Mapper.Initialize(config =>
            {
                DomainLogicAutomapperConfig.RegisterRules(config);

                config.AddProfile<CommonMapping>();
                config.AddProfile(new DevicesMapping(appSettings));
            });
        }
    }
}