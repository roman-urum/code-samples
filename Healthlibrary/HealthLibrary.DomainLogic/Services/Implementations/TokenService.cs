using System;
using System.Threading.Tasks;
using HealthLibrary.ApiAccess.DataProviders.Interfaces;
using HealthLibrary.Domain.Dtos.TokenService;
using HealthLibrary.DomainLogic.Services.Interfaces;

namespace HealthLibrary.DomainLogic.Services.Implementations
{
    /// <summary>
    /// Contains business rules to validate authorization data.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly ITokenDataProvider tokenDataProvider;

        public TokenService(ITokenDataProvider tokenDataProvider)
        {
            this.tokenDataProvider = tokenDataProvider;
        }

        /// <summary>
        /// Returns true if token has access to specified service.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> VerifyAccess(VerifyTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.Service))
            {
                throw new ArgumentNullException("request.Service");
            }

            VerifyTokenResponse result = await this.tokenDataProvider.VerifyToken(request);

            return result != null && result.Allowed;
        }

        /// <summary>
        /// Send request to Token service to verify that device certificate
        /// has access to patient.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<VerifyCertificateResponse> VerifyCertificate(VerifyCertificateRequest request)
        {
            var response = await this.tokenDataProvider.VerifyCertificate(request);

            return response;
        }
    }
}
