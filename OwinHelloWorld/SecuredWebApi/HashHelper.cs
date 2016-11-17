using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecuredWebApi
{
    public static class HashHelper
    {
        public static string ToMD5Hash(this byte[] bytes)
        {
            var hash = new StringBuilder();
            using (MD5 md5 = MD5.Create())
            {
                md5.ComputeHash(bytes).ToList().ForEach(b => hash.AppendFormat("{0:x2}", b));
            }
            return hash.ToString();
        }

        public static string ToMD5Hash(this string inputString)
        {
            return Encoding.UTF8.GetBytes(inputString).ToMD5Hash();
        }
    }
}
