using LightInject;
using Maestro.Common;
using Maestro.Web.Areas.Customer.Managers.Implementations;
using Maestro.Web.Areas.Customer.Managers.Interfaces;
using Maestro.Web.Areas.Site;
using Maestro.Web.Areas.Site.Managers.Implementations;
using Maestro.Web.Areas.Site.Managers.Interfaces;
using Maestro.Web.Helpers;
using Maestro.Web.Managers.Implementations;
using Maestro.Web.Managers.Interfaces;
using Maestro.Web.Security;
using Postal;
using WebApi.OutputCache.Core.Cache;

namespace Maestro.Web
{
    /// <summary>
    /// WebCompositionRoot.
    /// </summary>
    public class WebCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Composes services by adding services to the <paramref name="serviceRegistry" />.
        /// </summary>
        /// <param name="serviceRegistry">The target <see cref="T:LightInject.IServiceRegistry" />.</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IEmailService, EmailService>();
            serviceRegistry.Register<IEmailManager, EmailManager>();

            serviceRegistry.Register<IBaseAuthTokenStorage, SessionAuthDataStorage>();
            serviceRegistry.Register<IAuthDataStorage, SessionAuthDataStorage>();
            serviceRegistry.Register<IAuthenticator, Authenticator>();

            serviceRegistry.Register<ICustomerContext, DefaultCustomerContext>(new PerScopeLifetime());
            serviceRegistry.Register<IPatientContext, DefaultPatientContext>(new PerScopeLifetime());
            serviceRegistry.Register<ISiteContext, DefaultSiteContext>(new PerScopeLifetime());
            serviceRegistry.Register<ICareBuilderControllerManager, CareBuilderControllerManager>();

            serviceRegistry.Register<IApiOutputCache, StackExchangeRedisOutputCacheProvider>();

            #region Controllers Managers

            serviceRegistry.Register<IAdminsControllerManager, AdminsControllerManager>();
            serviceRegistry.Register<ICustomerUsersManager, CustomerUsersManager>();
            serviceRegistry.Register<IUsersControllerManager, UsersControllerManager>();
            serviceRegistry.Register<ISettingsControllerManager, SettingsControllerManager>();
            serviceRegistry.Register<IPatientsControllerManager, PatientsControllerManager>();
            serviceRegistry.Register<ICareBuilderControllerManager, CareBuilderControllerManager>();
            serviceRegistry.Register<ISitesControllerManager, SitesControllerManager>();

            #endregion

            serviceRegistry.Register<IDashboardControllerHelper, DashboardControllerHelper>();
        }
    }
}