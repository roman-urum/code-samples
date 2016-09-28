using System.Data.Entity;
using System.Linq;
using DeviceService.DataAccess.Context;
using DeviceService.DataAccess.Interceptors;
using DeviceService.DataAccess.Migrations;
using Isg.EntityFramework.Interceptors;
using LightInject;

namespace DeviceService.DataAccess
{
    public class DataAccessCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IUnitOfWork, UnitOfWork>(new PerScopeLifetime());
            serviceRegistry.Register<DbContext, DeviceServiceDbContext>(new PerScopeLifetime());
            serviceRegistry.Initialize(
                registration => registration.ServiceType == typeof(DbContext),
                (factory, instance) => Database.SetInitializer(
                    new MigrateDatabaseToLatestVersion<DeviceServiceDbContext, Configuration>()));

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