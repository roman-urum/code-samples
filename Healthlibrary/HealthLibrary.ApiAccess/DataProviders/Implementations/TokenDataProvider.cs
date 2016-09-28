using System.Threading.Tasks;
using HealthLibrary.ApiAccess.ApiClient;
using HealthLibrary.ApiAccess.DataProviders.Interfaces;
using HealthLibrary.Common;
using HealthLibrary.Common.Exceptions;
using HealthLibrary.Domain.Dtos.TokenService;
using RestSharp;

namespace HealthLibrary.ApiAccess.DataProviders.Implementations
{
    /// <summary>
    /// Provides access to TokenService methods.
    /// </summary>
    public class TokenDataProvider : ITokenDataProvider
    {
        private readonly IRestApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDataProvider"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public TokenDataProvider(IRestApiClientFactory factory)
        {
            _apiClient = factory.Create(Settings.TokenServiceUrl);
        }

        /// <summary>
        /// Returns info if token has access to specified service.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<VerifyTokenResponse> VerifyToken(VerifyTokenRequest request)
        {
            try
            {
                return await _apiClient.SendRequestAsync<VerifyTokenResponse>("api/tokens/{Id}", request, Method.GET);
            }
            catch (ServiceNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// Send request to Token service to verify that device certificate
        /// has access to patient.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<VerifyCertificateResponse> VerifyCertificate(VerifyCertificateRequest request)
        {
            return
                await
                    _apiClient.SendRequestAsync<VerifyCertificateResponse>("api/certificates/{Thumbprint}", request,
                        Method.GET);
        }
    }
}
