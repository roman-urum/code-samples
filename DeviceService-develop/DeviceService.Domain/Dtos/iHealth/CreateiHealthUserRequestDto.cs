using DeviceService.Common.ApiClient;

namespace DeviceService.Domain.Dtos.iHealth
{
    public class CreateiHealthUserRequestDto
    {
        [RequestParameter(RequestParameterType.RequestBody, "redirect_uri")]
        public string RedirectUri { get; set; }

        [RequestParameter(RequestParameterType.RequestBody)]
        public string UserName { get; set; }

        [RequestParameter(RequestParameterType.RequestBody)]
        public string UserPassword { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "sc")]
        public string Sc { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "sv")]
        public string Sv { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "client_id")]
        public string ClientId { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "client_secret")]
        public string ClientSecret { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "nickname")]
        public string Nickname { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "client_para")]
        public string ClientPara { get; set; }

        [RequestParameter(RequestParameterType.RequestBody)]
        public string APIName { get; set; }
    }
}
