using System;
using DeviceService.Common.ApiClient;

namespace DeviceService.Domain.Dtos.TokenService
{
    public class CreateCertificateRequest
    {
        [RequestParameter(RequestParameterType.RequestBody)]
        public int CustomerId { get; set; }

        [RequestParameter(RequestParameterType.RequestBody)]
        public Guid PatientId { get; set; }

        [RequestParameter(RequestParameterType.RequestBody)]
        public string Thumbprint { get; set; }

        /// <summary>
        /// Certificate in base-64 string.
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody)]
        public string Certificate { get; set; }
    }
}
