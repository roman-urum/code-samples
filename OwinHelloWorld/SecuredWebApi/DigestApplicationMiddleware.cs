using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;

namespace SecuredWebApi
{
    public class DigestApplicationMiddleware: AuthenticationMiddleware<DigestAuthenticationOptions>
    {
        public DigestApplicationMiddleware(OwinMiddleware next, DigestAuthenticationOptions options) : base(next, options)
        {

        }

        protected override AuthenticationHandler<DigestAuthenticationOptions> CreateHandler()
        {
            return new DigestAthenticationHandler();
        }
    }
}
