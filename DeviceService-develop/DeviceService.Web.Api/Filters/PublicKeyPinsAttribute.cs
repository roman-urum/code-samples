using System;
using System.Web.Http.Filters;
using DeviceService.Common.Security;
using DeviceService.Web.Api.Security;

namespace DeviceService.Web.Api.Filters
{
    /// <summary>
    /// Includes header with server certificate in response of each action
    /// to which attribute is applied.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PublicKeyPinsAttribute : ActionFilterAttribute
    {
        private const string PublicKeyPinsHeader = "Public-Key-Pins";

        /// <summary>
        /// Identifies if header certificate should be included
        /// in any case.
        /// </summary>
        public bool UseForAnonymousRequests { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (UseForAnonymousRequests || !CertificateAuthContext.Current.IsAuthorizedRequest)
            {
                return;
            }

            var certificate = SertificateGenerator.ServerCertificate;
            var certificateString = Convert.ToBase64String(certificate.GetRawCertData());

            actionExecutedContext.Response.Headers.Add(PublicKeyPinsHeader, certificateString);
        }
    }
}