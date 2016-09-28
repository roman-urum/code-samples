using VitalsService.ApiClient;

namespace VitalsService.Domain.Dtos.TokenServiceDtos
{
    public class GetTokenRequest
    {
        [RequestParameter(RequestParameterType.RequestBody)]
        public string Username { get; set; }

        [RequestParameter(RequestParameterType.RequestBody)]
        public string Password { get; set; }
    }
}
