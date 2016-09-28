using DeviceService.Common.ApiClient;

namespace DeviceService.Domain.Dtos.TokenService
{
    public class GetTokenRequest
    {
        [RequestParameter(RequestParameterType.RequestBody)]
        public string Username { get; set; }

        [RequestParameter(RequestParameterType.RequestBody)]
        public string Password { get; set; }
    }
}
