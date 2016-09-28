using System.Data.Entity;
using System.Linq;
using CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess.Contexts;
using CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess.Interceptors;
using CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess.Migrations;
using Isg.EntityFramework.Interceptors;
using LightInject;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess
{
    /// <summary>
    /// DataAccessCompositionRoot.
    /// </summary>
    public class DataAccessCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ITokenServiceDbContext, TokenServiceDbContext>(new PerScopeLifetime());
            serviceRegistry.Register<IUnitOfWork, UnitOfWork>(new PerScopeLifetime());

            serviceRegistry.Initialize(
                registration => registration.ServiceType == typeof(ITokenServiceDbContext),
                (factory, instance) => Database.SetInitializer(
                    new MigrateDatabaseToLatestVersion<TokenServiceDbContext, Configuration>()));

            serviceRegistry.Register<IInterceptor, PrimaryKeyInterceptor>("PrimaryKeyInterceptor", new PerContainerLifetime());
            serviceRegistry.Register<IInterceptor, SoftDeletableInterceptor>("SoftDeletableInterceptor", new PerContainerLifetime());
            serviceRegistry.Register<IInterceptorProvider>(
                factory => new DefaultInterceptorProvider(factory.GetAllInstances<IInterceptor>().ToArray()),
                new PerContainerLifetime());
        }
    }
}