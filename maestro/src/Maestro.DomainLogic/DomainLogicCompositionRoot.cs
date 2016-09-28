using LightInject;
using Maestro.DomainLogic.Services.Implementations;
using Maestro.DomainLogic.Services.Interfaces;

namespace Maestro.DomainLogic
{
    /// <summary>
    /// DomainLogicCompositionRoot.
    /// </summary>
    /// <seealso cref="LightInject.ICompositionRoot" />
    public class DomainLogicCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Composes services by adding services to the <paramref name="serviceRegistry" />.
        /// </summary>
        /// <param name="serviceRegistry">The target <see cref="T:LightInject.IServiceRegistry" />.</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            #region #region Services

            serviceRegistry.Register<IUsersService, UsersService>();
            serviceRegistry.Register<ICustomersService, CustomersService>();
            serviceRegistry.Register<ICustomerUsersService, CustomerUsersService>();
            serviceRegistry.Register<IPatientsService, PatientsService>();
            serviceRegistry.Register<IDevicesService, DevicesService>();
            serviceRegistry.Register<ITokenService, TokenService>();
            serviceRegistry.Register<IHealthLibraryService, HealthLibraryService>();
            serviceRegistry.Register<IVitalsService, VitalsService>();
            serviceRegistry.Register<ITrendsSettingsService, TrendsSettingsService>();
            serviceRegistry.Register<INotesService, NotesService>();
            serviceRegistry.Register<IZoomService, ZoomService>();
            serviceRegistry.Register<IMessagingHubService, MessagingHubService>();
            serviceRegistry.Register<IVitalConverter, VitalConverter>(new PerScopeLifetime());

            #endregion
        }
    }
}