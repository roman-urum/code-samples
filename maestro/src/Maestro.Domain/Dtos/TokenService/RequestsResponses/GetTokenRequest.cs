using Maestro.Common.ApiClient;

namespace Maestro.Domain.Dtos.TokenService.RequestsResponses
{
    public class GetTokenRequest
    {
        [RequestParameter(RequestParameterType.RequestBody)]
        public string Username { get; set; }

        [RequestParameter(RequestParameterType.RequestBody)]
        public string Password { get; set; }
    }
}
