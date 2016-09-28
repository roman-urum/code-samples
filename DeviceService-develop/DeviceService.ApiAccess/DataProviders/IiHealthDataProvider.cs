using System.Threading.Tasks;
using DeviceService.Domain.Dtos.iHealth;

namespace DeviceService.ApiAccess.DataProviders
{
    public interface IiHealthDataProvider
    {
        /// <summary>
        /// Creates new account in iHealth API.
        /// </summary>
        /// <returns></returns>
        Task<iHealthUserResponseDto> RegisterUser(CreateiHealthUserRequestDto requestDto);
    }
}