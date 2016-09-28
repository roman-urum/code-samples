using System.Security.Cryptography;
using System.Web;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Helpers
{
    /// <summary>
    /// Provides methods to generate tokens.
    /// </summary>
    public static class TokensGenerator
    {
        /// <summary>
        /// Generates new string with token.
        /// </summary>
        public static string GetToken()
        {
            var bytes = new byte[32];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(bytes);
            }

            return HttpServerUtility.UrlTokenEncode(bytes);
        }
    }
}