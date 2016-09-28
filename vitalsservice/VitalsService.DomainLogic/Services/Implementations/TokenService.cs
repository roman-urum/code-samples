using System;
using System.Threading.Tasks;
using Vitals.ApiAccess.DataProviders;
using VitalsService.Domain.Dtos.TokenServiceDtos;
using VitalsService.DomainLogic.Services.Interfaces;

namespace VitalsService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// TokenService.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IUsersDataProvider usersDataProver;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="usersDataProver">The users data prover.</param>
        public TokenService(IUsersDataProvider usersDataProver)
        {
            this.usersDataProver = usersDataProver;
        }

        /// <summary>
        /// Checks the access.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        public async Task<VerifyAccessResponse> CheckAccess(string route)
        {
            var response = await usersDataProver.VerifyAccess(route);

            return response;
        }

        /// <summary>
        /// Returns true if token has access to specified service.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> CheckAccess(VerifyTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.Service))
            {
                throw new ArgumentNullException("request.Service");
            }

            VerifyAccessResponse result = await this.usersDataProver.VerifyAccess(request);

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
            var response = await usersDataProver.VerifyCertificate(request);

            return response;
        }
    }
}
