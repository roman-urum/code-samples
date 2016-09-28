using System.Configuration;
using System.Data.Entity;
using System.Linq;
using Isg.EntityFramework.Interceptors;
using LightInject;
using VitalsService.DataAccess.EF.Contexts;
using VitalsService.DataAccess.EF.Interceptors;
using Configuration = VitalsService.DataAccess.EF.Migrations.Configuration;

namespace VitalsService.DataAccess.EF
{
    public class EfCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Initializes implementations for data access/ef layer.
        /// </summary>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<DbContext, VitalsDataContext>(new PerScopeLifetime());
            serviceRegistry.Initialize(
                registration => registration.ServiceType == typeof(DbContext),
                (factory, instance) =>
                {
                    var connectionStringName = string.Format("Vitals_Customer_{0}",
                        factory.GetInstance<ICustomerContext>().CustomerId);

                    if (ConfigurationManager.ConnectionStrings[connectionStringName] == null)
                    {
                        connectionStringName = "Vitals_Shared";
                    }

                    Database.SetInitializer(new MigrateDatabaseToLatestVersion<VitalsDataContext, Configuration>(connectionStringName));
                });
            serviceRegistry.Register<IUnitOfWork, UnitOfWork>(new PerScopeLifetime());

            serviceRegistry.Register<IInterceptor, PrimaryKeyInterceptor>("PrimaryKeyInterceptor", new PerContainerLifetime());
            serviceRegistry.Register<IInterceptor, AuditableInterceptor>("AuditableInterceptor", new PerContainerLifetime());

            serviceRegistry.Register<IInterceptorProvider>(
                factory => new DefaultInterceptorProvider(factory.GetAllInstances<IInterceptor>().ToArray()), new PerContainerLifetime()
            );
        }
    }
}