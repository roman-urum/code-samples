using System;
using HealthLibrary.Common.ApiClient;

namespace HealthLibrary.Domain.Dtos.TokenService
{
    public class VerifyCertificateRequest
    {
        [RequestParameter(RequestParameterType.QueryString)]
        public int CustomerId { get; set; }

        [RequestParameter(RequestParameterType.QueryString)]
        public Guid? PatientId { get; set; }

        [RequestParameter(RequestParameterType.UrlSegment)]
        public string Thumbprint { get; set; }
    }
}
