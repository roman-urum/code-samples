using DeviceService.ApiAccess.ApiClient;
using LightInject;
using Vitals.ApiAccess.ApiClient;
using Vitals.ApiAccess.DataProviders;
using Vitals.ApiAccess.DataProviders.Implementation;

namespace Vitals.ApiAccess
{
    public class ApiAccessCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            #region Tools

            serviceRegistry.Register<IRestApiClientFactory, RestApiClientFactory>();

            #endregion

            #region Data Providers

            serviceRegistry.Register<IUsersDataProvider, UsersDataProvider>();
            serviceRegistry.Register<IMessagingHubDataProvider, MessagingHubDataProvider>();

            #endregion
        }
    }
}
