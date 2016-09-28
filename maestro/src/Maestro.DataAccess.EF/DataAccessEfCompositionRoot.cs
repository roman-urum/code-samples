using System.Data.Entity;
using System.Linq;
using Isg.EntityFramework.Interceptors;
using LightInject;
using Maestro.DataAccess.EF.Context;
using Maestro.DataAccess.EF.DataAccess;
using Maestro.DataAccess.EF.Interceptors;
using Maestro.DataAccess.EF.Migrations;

namespace Maestro.DataAccess.EF
{
    /// <summary>
    /// DataAccessEfCompositionRoot.
    /// </summary>
    /// <seealso cref="LightInject.ICompositionRoot" />
    public class DataAccessEfCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Composes services by adding services to the <paramref name="serviceRegistry" />.
        /// </summary>
        /// <param name="serviceRegistry">The target <see cref="T:LightInject.IServiceRegistry" />.</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IUnitOfWork, UnitOfWork>(new PerScopeLifetime());

            serviceRegistry.Register<DbContext, MaestroDbContext>(new PerScopeLifetime());
            serviceRegistry.Initialize(
                registration => registration.ServiceType == typeof(DbContext),
                (factory, instance) => Database.SetInitializer(
                    new MigrateDatabaseToLatestVersion<MaestroDbContext, Configuration>()));

            serviceRegistry.Register<IInterceptor, PrimaryKeyInterceptor>("PrimaryKeyInterceptor", new PerContainerLifetime());
            serviceRegistry.Register<IInterceptor, SoftDeletableInterceptor>("SoftDeletableInterceptor", new PerContainerLifetime());
            serviceRegistry.Register<IInterceptorProvider>(
                factory =>
                    new DefaultInterceptorProvider(factory.GetAllInstances<IInterceptor>().ToArray()),
                new PerContainerLifetime()
            );
        }
    }
}