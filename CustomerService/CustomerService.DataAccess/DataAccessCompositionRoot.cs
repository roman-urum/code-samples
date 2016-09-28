using System.Data.Entity;
using System.Linq;
using CustomerService.DataAccess.Context;
using CustomerService.DataAccess.Interceptors;
using CustomerService.DataAccess.Migrations;
using Isg.EntityFramework.Interceptors;
using LightInject;

namespace CustomerService.DataAccess
{
    /// <summary>
    /// DataAccessCompositionRoot.
    /// </summary>
    /// <seealso cref="LightInject.ICompositionRoot" />
    public class DataAccessCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Composes services by adding services to the <paramref name="serviceRegistry" />.
        /// </summary>
        /// <param name="serviceRegistry">The target <see cref="T:LightInject.IServiceRegistry" />.</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ICustomerServiceDbContext, CustomerServiceDbContext>(new PerScopeLifetime());
            serviceRegistry.Initialize(
                registration => registration.ServiceType == typeof(ICustomerServiceDbContext),
                (factory, instance) => Database.SetInitializer(
                    new MigrateDatabaseToLatestVersion<CustomerServiceDbContext, Configuration>()));
            serviceRegistry.Register<IUnitOfWork, UnitOfWork>(new PerScopeLifetime());
            serviceRegistry.Register<IInterceptor, PrimaryKeyInterceptor>("PrimaryKeyInterceptor", new PerContainerLifetime());
            serviceRegistry.Register<IInterceptor, SoftDeletableInterceptor>("SoftDeletableInterceptor", new PerContainerLifetime());
            serviceRegistry.Register<IInterceptorProvider>(
                factory => new DefaultInterceptorProvider(factory.GetAllInstances<IInterceptor>().ToArray()), new PerContainerLifetime());
        }
    }
}