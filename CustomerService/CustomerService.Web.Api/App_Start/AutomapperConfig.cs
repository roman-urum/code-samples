using AutoMapper;
using CustomerService.DomainLogic;
using CustomerService.Web.Api.Models.Mapping;

namespace CustomerService.Web.Api
{
    /// <summary>
    /// AutomapperConfig.
    /// </summary>
    public static class AutomapperConfig
    {
        /// <summary>
        /// Registers the rules.
        /// </summary>
        public static void RegisterRules()
        {
            Mapper.Initialize(config =>
            {
                DomainLogicAutomapperConfig.RegisterRules(config);

                config.AddProfile<CommonMapping>();
                config.AddProfile<CustomersMapping>();
                config.AddProfile<SitesMapping>();
                config.AddProfile<CategoriesOfCareMapping>();
                config.AddProfile<CategoriesOfCareMapping>();
                config.AddProfile<OrganizationsMapping>();
            });
        }
    }
}