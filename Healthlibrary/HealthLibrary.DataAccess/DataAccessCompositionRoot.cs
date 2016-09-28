using System.Configuration;
using System.Data.Entity;
using System.Linq;
using HealthLibrary.Common;
using HealthLibrary.DataAccess.Contexts;
using HealthLibrary.DataAccess.Interceptors;
using Isg.EntityFramework.Interceptors;
using LightInject;
using Configuration = HealthLibrary.DataAccess.Migrations.Configuration;

namespace HealthLibrary.DataAccess
{
    /// <summary>
    /// DataAccessCompositionRoot.
    /// </summary>
    public class DataAccessCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Composes services by adding services to the <paramref name="serviceRegistry" />.
        /// </summary>
        /// <param name="serviceRegistry">The target <see cref="T:LightInject.IServiceRegistry" />.</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IUnitOfWork, UnitOfWork>(new PerScopeLifetime());
            serviceRegistry.Initialize(
                registration => registration.ServiceType == typeof(IUnitOfWork),
                (factory, instance) =>
                {
                    var connectionStringName = string.Format("HealthLibraryService_{0}",
                        factory.GetInstance<ICareElementContext>().CustomerId);

                    if (ConfigurationManager.ConnectionStrings[connectionStringName] == null)
                    {
                        connectionStringName = "HealthLibraryService_Shared";
                    }

                    Database.SetInitializer(new MigrateDatabaseToLatestVersion<HealthLibraryServiceDbContext, Configuration>(connectionStringName));
                });

            serviceRegistry.Register<IInterceptor, PrimaryKeyInterceptor>("PrimaryKeyInterceptor", new PerContainerLifetime());
            serviceRegistry.Register<IInterceptor, SoftDeletableInterceptor>("SoftDeletableInterceptor", new PerContainerLifetime());
            serviceRegistry.Register<IInterceptor, AuditableInterceptor>("AuditableInterceptor", new PerContainerLifetime());

            serviceRegistry.Register<IInterceptorProvider>(
                factory => new DefaultInterceptorProvider(factory.GetAllInstances<IInterceptor>().ToArray()), new PerContainerLifetime()
            );
        }
    }
}