using CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess;
using CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Implementations;
using CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Interfaces;
using LightInject;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic
{
    public class DomainLogicCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Composes services by adding services to the <paramref name="serviceRegistry" />.
        /// </summary>
        /// <param name="serviceRegistry">The target <see cref="T:LightInject.IServiceRegistry" />.</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterFrom<DataAccessCompositionRoot>();

            serviceRegistry.Register<ICertificatesService, CertificatesService>();
            serviceRegistry.Register<IGroupsService, GroupsService>();
            serviceRegistry.Register<IPrincipalsService, PrincipalsService>();
        }
    }
}
