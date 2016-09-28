using DeviceService.ApiAccess.DataProviders;
using LightInject;
using DeviceService.ApiAccess.ApiClient;
using DeviceService.ApiAccess.DataProviders.Implementation;

namespace DeviceService.ApiAccess
{
    public class ApiAccessCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            #region Tools

            serviceRegistry.Register<IRestApiClientFactory, RestApiClientFactory>();
            serviceRegistry.Register<IiHealthSettings, iHealthSettings>();

            #endregion

            #region Data Providers

            serviceRegistry.Register<IUsersDataProvider, UsersDataProvider>();
            serviceRegistry.Register<IMessagingHubDataProvider, MessagingHubDataProvider>();
            serviceRegistry.Register<IiHealthDataProvider, iHealthDataProvider>();

            #endregion
        }
    }
}
