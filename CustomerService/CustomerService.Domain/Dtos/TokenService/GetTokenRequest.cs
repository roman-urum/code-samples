using CustomerService.Common.ApiClient;

namespace CustomerService.Domain.Dtos.TokenService
{
    public class GetTokenRequest
    {
        [RequestParameter(RequestParameterType.RequestBody)]
        public string Username { get; set; }

        [RequestParameter(RequestParameterType.RequestBody)]
        public string Password { get; set; }
    }
}
