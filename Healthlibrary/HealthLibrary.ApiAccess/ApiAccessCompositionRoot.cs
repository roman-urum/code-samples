using HealthLibrary.ApiAccess.ApiClient;
using HealthLibrary.ApiAccess.DataProviders.Implementations;
using HealthLibrary.ApiAccess.DataProviders.Interfaces;
using LightInject;

namespace HealthLibrary.ApiAccess
{
    public class ApiAccessCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            #region Tools

            serviceRegistry.Register<IRestApiClientFactory, RestApiClientFactory>();

            #endregion

            #region Data Providers

            serviceRegistry.Register<ITokenDataProvider, TokenDataProvider>();

            #endregion
        }
    }
}
