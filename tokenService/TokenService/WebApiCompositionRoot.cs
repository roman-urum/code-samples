using System.Configuration;
using CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Implementations;
using CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Interfaces;
using LightInject;
using StackExchange.Redis;

namespace CareInnovations.HealthHarmony.Maestro.TokenService
{
    /// <summary>
    /// LightInject composition root for Api.
    /// </summary>
    public class WebApiCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Composes services by adding services to the <paramref name="serviceRegistry" />.
        /// </summary>
        /// <param name="serviceRegistry">The target <see cref="T:LightInject.IServiceRegistry" />.</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ConnectionMultiplexer>(factory =>
                ConnectionMultiplexer.Connect(
                    ConfigurationManager.ConnectionStrings["cache"].ConnectionString, null),
                new PerContainerLifetime());

            serviceRegistry.Register<IDatabase>(factory => 
                factory.GetInstance<ConnectionMultiplexer>().GetDatabase(-1, null));

            serviceRegistry.Register<ICertificatesControllerHelper, CertificatesControllerHelper>();
            serviceRegistry.Register<IGroupsControllerHelper, GroupsControllerHelper>();
            serviceRegistry.Register<IPrincipalsControllerHelper, PrincipalsControllerHelper>();
            serviceRegistry.Register<ITokensControllerHelper, TokensControllerHelper>();
        }
    }
}