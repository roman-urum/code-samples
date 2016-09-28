using DeviceService.ApiAccess;
using DeviceService.DomainLogic.Services.Interfaces;
using LightInject;

namespace DeviceService.DomainLogic
{
    public class DomainLogicCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterFrom<ApiAccessCompositionRoot>();
            serviceRegistry.Register<IDevicesService, Services.Implementations.DevicesService>();
        }
    }
}