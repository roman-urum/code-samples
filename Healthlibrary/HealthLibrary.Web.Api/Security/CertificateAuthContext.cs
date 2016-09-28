using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using HealthLibrary.Common.Exceptions;
using HealthLibrary.Domain.Dtos.TokenService;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.Web.Api.Extensions;
using Microsoft.Practices.ServiceLocation;

namespace HealthLibrary.Web.Api.Security
{
    /// <summary>
    /// Container for authorization certificate context.
    /// </summary>
    public class CertificateAuthContext
    {
        /// <summary>
        /// Returns instance for current authorization context.
        /// </summary>
        public static ICertificateAuthContext Current
        {
            get { return ServiceLocator.Current.GetInstance<ICertificateAuthContext>(); }
        }
    }

    /// <summary>
    /// Provides request auth data.
    /// </summary>
    public interface ICertificateAuthContext
    {
        /// <summary>
        /// Identifies if request contains client certificate.
        /// </summary>
        bool IsAuthorizedRequest { get; }

        /// <summary>
        /// Verifies if certificate has access to specified patient.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task<bool> HasAccess(int customerId, Guid? patientId = null);
    }

    /// <summary>
    /// Default implementation of ICertificateAuthRequest
    /// </summary>
    public class DefaultCertificateAuthContext : ICertificateAuthContext
    {
        private readonly string thumbprint;
        private readonly ITokenService tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultCertificateAuthContext"/> class.
        /// </summary>
        /// <param name="tokenService">The token service.</param>
        public DefaultCertificateAuthContext(ITokenService tokenService)
        {
            this.tokenService = tokenService;
            X509Certificate2 certificate = HttpContext.Current.Request.GetClientCertificate();

            if (certificate == null)
            {
                return;
            }

            thumbprint = certificate.Thumbprint;
            IsAuthorizedRequest = true;
        }

        /// <summary>
        /// Identifies if request contains client certificate.
        /// </summary>
        public bool IsAuthorizedRequest { get; private set; }

        /// <summary>
        /// Verifies if certificate has access to specified patient.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<bool> HasAccess(int customerId, Guid? patientId = null)
        {
            var request = new VerifyCertificateRequest
            {
                Thumbprint = this.thumbprint,
                CustomerId = customerId,
                PatientId = patientId
            };

            try
            {
                VerifyCertificateResponse result = await tokenService.VerifyCertificate(request);

                return result.IsAllowed;
            }
            catch (ServiceNotFoundException)
            {
                return false;
            }
        }
    }
}