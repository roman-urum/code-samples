using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain.Dtos.TokenService.RequestsResponses;
using Maestro.DomainLogic.Services.Interfaces;

namespace Maestro.DomainLogic.Services.Implementations
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
        public VerifyAccessResponse CheckAccess(string route)
        {
            var response = usersDataProver.VerifyAccess(route);

            return response;
        }
    }
}