using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace SecuredWebApi
{
    public class DigestAthenticationHandler: AuthenticationHandler<DigestAuthenticationOptions>
    {
        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var ticket = new AuthenticationTicket(null, null);

            var authHeader = Request.Headers.Get("Authorization");
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Digest", StringComparison.OrdinalIgnoreCase))
            {
                string parameter = authHeader.Substring("Digest".Length).Trim();
                string userName;
                if (DigestAuthenticator.TryAuthenticate(parameter, Request.Method, out userName))
                {
                    var identity = new ClaimsIdentity(new []
                    {
                        new Claim(ClaimTypes.Name, userName), 
                        new Claim(ClaimTypes.NameIdentifier, userName)
                    }, "Digest");
                    ticket = new AuthenticationTicket(identity, null);
                }
            }

            return Task.FromResult(ticket);
        }

        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode == 401)
            {
                Response.Headers.AppendValues("WWW-Authenticate", $"Digest realm={Options.Realm}, nonce={Options.GenerateNonceBytes().ToMD5Hash()}");
            }

            return Task.FromResult<object>(null);
        }
    }
}
