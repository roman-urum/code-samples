namespace PatientService.ApiAccess.DataProviders
{
    using System.Threading.Tasks;

    using PatientService.Domain.DeviceServiceDtos;

    public interface IDevicesDataProvider
    {
        Task CreateDevice(CreateDeviceRequestDto createDeviceRequest);
    }
}