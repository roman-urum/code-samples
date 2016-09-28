using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace HealthLibrary.Web.Api.Extensions
{
    /// <summary>
    /// Extension methods for HttpRequest objects.
    /// </summary>
    public static class HttpRequestExtensions
    {
        private const int AuthHeaderPartsCount = 2;
        private const int AuthTypeIndex = 0;
        private const int AuthTokenIndex = 1;

        private const char AuthTypeSeparator = ' ';

        private const string RequiredAuthType = "Bearer";
        private const string CertificateHeaderName = "X-ARR-ClientCert";

        /// <summary>
        /// Returns auth token specified in request.
        /// Returns null if request token not provided.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetAuthToken(this HttpRequest request)
        {
            var authHeader = request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authHeader))
            {
                return null;
            }

            IEnumerable<string> authHeaderParts = authHeader.Split(AuthTypeSeparator);

            if (authHeaderParts.Count() != AuthHeaderPartsCount ||
                !authHeaderParts.ElementAt(AuthTypeIndex).Equals(RequiredAuthType))
            {
                return null;
            }

            return authHeaderParts.ElementAt(AuthTokenIndex);
        }

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