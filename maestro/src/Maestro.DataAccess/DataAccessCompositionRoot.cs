using LightInject;
using Maestro.DataAccess.Api.ApiClient;
using Maestro.DataAccess.Api.DataProviders.Implementations;
using Maestro.DataAccess.Api.DataProviders.Interfaces;

namespace Maestro.DataAccess.Api
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
            #region Tools

            serviceRegistry.Register<IRestApiClientFactory, RestApiClientFactory>();

            #endregion

            #region

            serviceRegistry.Register<IUsersDataProvider, UsersDataProvider>();
            serviceRegistry.Register<ICustomersDataProvider, CustomersDataProvider>();
            serviceRegistry.Register<IPatientsDataProvider, PatientsDataProvider>();
            serviceRegistry.Register<IDevicesDataProvider, DevicesDataProvider>();
            serviceRegistry.Register<IHealthLibraryDataProvider, HealthLibraryDataProvider>();
            serviceRegistry.Register<IVitalsDataProvider, VitalsDataProvider>();
            serviceRegistry.Register<INotesDataProvider, NotesDataProvider>();
            serviceRegistry.Register<IZoomDataProvider, ZoomDataProvider>();
            serviceRegistry.Register<IMessagingHubDataProvider, MessagingHubDataProvider>();

            #endregion
        }
    }
}