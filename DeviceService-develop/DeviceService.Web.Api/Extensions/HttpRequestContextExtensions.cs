using System;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace DeviceService.Web.Api.Extensions
{
    /// <summary>
    /// Provides extension methods for Http
    /// </summary>
    public static class HttpRequestContextExtensions
    {
        private const string CertificateHeaderName = "X-ARR-ClientCert";

        /// <summary>
        /// Reads client certificate from header or
        /// from request client certificate.
        /// </summary>
        /// <returns></returns>
        public static X509Certificate2 GetClientCertificate(this HttpRequest request)
        {
            string clientCertificateHeader = request.Headers[CertificateHeaderName];
            X509Certificate2 clientCertificate = null;

            if (!string.IsNullOrEmpty(clientCertificateHeader))
            {
                var bytes = Convert.FromBase64String(clientCertificateHeader);

                clientCertificate = new X509Certificate2(bytes);
            }
            else if (request.ClientCertificate.IsPresent)
            {
                clientCertificate = new X509Certificate2(request.ClientCertificate.Certificate);
            }

            return clientCertificate;
        }
    }
}