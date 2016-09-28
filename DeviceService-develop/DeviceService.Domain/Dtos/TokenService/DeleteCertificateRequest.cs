using DeviceService.Common.ApiClient;

namespace DeviceService.Domain.Dtos.TokenService
{
    public class DeleteCertificateRequest
    {
        [RequestParameter(RequestParameterType.UrlSegment)]
        public string Thumbprint { get; set; }
    }
}
