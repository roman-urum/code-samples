using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Maestro.Web.Helpers
{
    /// <summary>
    /// HmacGenerator.
    /// </summary>
    public static class HmacGenerator
    {
        private static readonly HMACSHA1 hmac =new HMACSHA1(new byte[]{1,2});

        /// <summary>
        /// Initializes the <see cref="HmacGenerator"/> class.
        /// </summary>
        static HmacGenerator()
        {
            hmac.Initialize();
        }

        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetHash(string value)
        {
            return HttpServerUtility.UrlTokenEncode(hmac.ComputeHash(Encoding.ASCII.GetBytes(value)));
        }
    }
}
