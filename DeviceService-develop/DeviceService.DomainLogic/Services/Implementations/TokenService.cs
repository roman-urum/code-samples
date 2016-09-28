using System.Threading.Tasks;
using DeviceService.ApiAccess.DataProviders;
using DeviceService.Domain.Dtos.TokenService;
using DeviceService.DomainLogic.Services.Interfaces;

namespace DeviceService.DomainLogic.Services.Implementations
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
    }
}