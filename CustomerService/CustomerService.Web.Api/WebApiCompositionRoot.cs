using System.Configuration;
using CustomerService.ApiAccess;
using CustomerService.Common;
using CustomerService.DataAccess;
using CustomerService.DomainLogic;
using CustomerService.DomainLogic.TokenService;
using CustomerService.Web.Api.Helpers;
using CustomerService.Web.Api.Helpers.Implemetations;
using CustomerService.Web.Api.Helpers.Interfaces;
using LightInject;
using StackExchange.Redis;
using WebApi.OutputCache.Core.Cache;

namespace CustomerService.Web.Api
{
    /// <summary>
    /// WebApiCompositionRoot.
    /// </summary>
    public class WebApiCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Composes services by adding services to the <paramref name="serviceRegistry" />.
        /// </summary>
        /// <param name="serviceRegistry">The target <see cref="T:LightInject.IServiceRegistry" />.</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterFrom<ApiAccessCompositionRoot>();
            serviceRegistry.RegisterFrom<DataAccessCompositionRoot>();
            serviceRegistry.RegisterFrom<DomainLogicCompositionRoot>();

            serviceRegistry.Register<ICustomersControllerHelper, CustomersControllerHelper>();
            serviceRegistry.Register<ISitesControllerHelper, SitesControllerHelper>();
            serviceRegistry.Register<IFileUploadControllerHelper, FileUploadControllerHelper>();
            serviceRegistry.Register<ICategoriesOfCareControllerHelper, CategoriesOfCareControllerHelper>();
            serviceRegistry.Register<IOrganizationsControllerHelper, OrganizationsControllerHelper>();

            serviceRegistry.Register<ITokenService, TokenService>();

            serviceRegistry.Register<ConnectionMultiplexer>(factory =>
                ConnectionMultiplexer.Connect(
                    ConfigurationManager.ConnectionStrings["RedisCache"].ConnectionString, null),
                new PerContainerLifetime());

            serviceRegistry.Register<IDatabase>(factory =>
                factory.GetInstance<ConnectionMultiplexer>().GetDatabase(-1, null));

            serviceRegistry.Register<IApiOutputCache, StackExchangeRedisOutputCacheProvider>();
        }
    }
}