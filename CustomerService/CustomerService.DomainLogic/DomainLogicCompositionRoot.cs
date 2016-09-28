using CustomerService.DomainLogic.Services.Implementations;
using CustomerService.DomainLogic.Services.Interfaces;
using LightInject;

namespace CustomerService.DomainLogic
{
    /// <summary>
    /// DomainLogicCompositionRoot.
    /// </summary>
    public class DomainLogicCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Composes the specified service registry.
        /// </summary>
        /// <param name="serviceRegistry">The service registry.</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ICustomerService, Services.Implementations.CustomersService>();
            serviceRegistry.Register<ISiteService, SitesService>();
            serviceRegistry.Register<ICategoriesOfCareService, CategoriesOfCareService>();
            serviceRegistry.Register<IOrganizationsService, OrganizationsService>();
        }
    }
}