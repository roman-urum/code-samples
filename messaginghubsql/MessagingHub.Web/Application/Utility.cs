using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace MessagingHub.Web
{
    public static class Utility
    {
        public static string CreateRngCspString(int length = 32)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var buffer = new byte[length];
                rng.GetNonZeroBytes(buffer);

                return Convert.ToBase64String(buffer);
            }
        }
    }
}