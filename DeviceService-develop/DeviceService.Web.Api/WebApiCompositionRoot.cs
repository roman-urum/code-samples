using System.Configuration;
using DeviceService.Common;
using DeviceService.DataAccess;
using DeviceService.DomainLogic;
using DeviceService.DomainLogic.Services.Implementations;
using DeviceService.DomainLogic.Services.Interfaces;
using DeviceService.Web.Api.Helpers;
using DeviceService.Web.Api.Helpers.Implementations;
using DeviceService.Web.Api.Helpers.Interfaces;
using DeviceService.Web.Api.Ioc;
using DeviceService.Web.Api.Models;
using DeviceService.Web.Api.Models.Mapping;
using DeviceService.Web.Api.Security;
using LightInject;
using StackExchange.Redis;
using WebApi.OutputCache.Core.Cache;

namespace DeviceService.Web.Api
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
            serviceRegistry.RegisterFrom<CommonCompositionRoot>();
            serviceRegistry.RegisterFrom<DataAccessCompositionRoot>();
            serviceRegistry.RegisterFrom<DomainLogicCompositionRoot>();

            serviceRegistry.Register<IDevicesControllerHelper, DevicesControllerHelper>();
            serviceRegistry.Register<ICheckInControllerHelper, CheckInControllerHelper>();
            serviceRegistry.Register<ICertificateAuthContext, DefaultCertificateAuthContext>(new PerHttpRequestLifeTime());
            serviceRegistry.Register<ICustomerContext, CustomerContext>(new PerScopeLifetime());

            serviceRegistry.Register<ITokenService, TokenService>();
            serviceRegistry.Register<IAppSettings, AppSettings>();

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