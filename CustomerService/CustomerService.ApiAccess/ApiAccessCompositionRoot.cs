using CustomerService.ApiAccess.ApiClient;
using CustomerService.ApiAccess.DataProviders;
using CustomerService.ApiAccess.DataProviders.Implementation;
using LightInject;

namespace CustomerService.ApiAccess
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

            #endregion
        }
    }
}
