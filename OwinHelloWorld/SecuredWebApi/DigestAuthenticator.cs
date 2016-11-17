using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SecuredWebApi
{
    public class DigestAuthenticator
    {
        public static bool TryAuthenticate(string headerParameter, string method, out string userName)
        {
            string realm = null;
            string nonce = null;
            string uri = null;
            string response = null;
            string user = null;

            string pattern = @"(\w+)=""([^""\\]*)""\s*(?:,\s*|$)";
            Regex.Replace(headerParameter, pattern, match =>
            {
                string key = match.Groups[1].Value.Trim();
                string value = match.Groups[2].Value.Trim();
                switch (key)
                {
                    case "username": user = value; break;
                    case "realm": realm = value; break;
                    case "nonce": nonce = value; break;
                    case "uri": uri = value; break;
                    case "response": response = value; break;
                }

                return string.Empty;
            });

            if (realm != null && user != null && nonce != null && uri != null && response != null)
            {
                string password = user;
                string ha1 = $"{user}:{realm}:{password}".ToMD5Hash();
                string ha2 = $"{method}:{uri}".ToMD5Hash();
                string computedResponse = $"{ha1}:{nonce}:{ha2}".ToMD5Hash();
                userName = user;
                return string.CompareOrdinal(response, computedResponse) == 0;
            }
            userName = null;
            return false;
        }
    }
}
