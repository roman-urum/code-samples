using System;
using System.Web.Http.Filters;
using VitalsService.Web.Api.Security;

namespace VitalsService.Web.Api.Filters
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
        /// Called when [action executed].
        /// </summary>
        /// <param name="actionExecutedContext">The action executed context.</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (!CertificateAuthContext.Current.IsAuthorizedRequest)
            {
                return;
            }

            var certificate = CertificateStorage.ServerCertificate;
            var certificateString = Convert.ToBase64String(certificate.GetRawCertData());

            actionExecutedContext.Response.Headers.Add(PublicKeyPinsHeader, certificateString);
        }
    }
}