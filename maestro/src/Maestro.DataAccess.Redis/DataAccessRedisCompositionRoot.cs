using System.Configuration;
using LightInject;
using StackExchange.Redis;

namespace Maestro.DataAccess.Redis
{
    /// <summary>
    /// DataAccessRedisCompositionRoot.
    /// </summary>
    public class DataAccessRedisCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Composes services by adding services to the <paramref name="serviceRegistry" />.
        /// </summary>
        /// <param name="serviceRegistry">The target <see cref="T:LightInject.IServiceRegistry" />.</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ConnectionMultiplexer>(factory =>
                ConnectionMultiplexer.Connect(
                    ConfigurationManager.ConnectionStrings["RedisCache"].ConnectionString, null),
                new PerContainerLifetime());

            serviceRegistry.Register<IDatabase>(factory =>
                factory.GetInstance<ConnectionMultiplexer>().GetDatabase(-1, null));

            serviceRegistry.Register<ICacheProvider, StackExchangeRedisCacheProvider>();
        }
    }
}