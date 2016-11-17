using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace SecuredWebApi
{
    public class DigestAuthenticationOptions: AuthenticationOptions
    {
        public DigestAuthenticationOptions() : base("Digest") {}

        public string Realm { get; set; }
        public Func<byte[]> GenerateNonceBytes { get; set; }
    }
}
