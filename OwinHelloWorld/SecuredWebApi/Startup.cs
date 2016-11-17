using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Security.Jwt;
using Owin;

namespace SecuredWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var jwtOptions = new JwtBearerAuthenticationOptions()
            //{
            //    AllowedAudiences = new [] { "http://localhost:5000/api" },
            //    IssuerSecurityTokenProviders = new []
            //    {
            //        new SymmetricKeyIssuerSecurityTokenProvider("http://authzserver.demoX", "tTW8HB0ebW1qpCmRUEOknEIxaTQ0BFCYrdjOdOl4rfM=")
            //    }
            //};
            //app.UseJwtBearerAuthentication(jwtOptions);

            var digestOptions = new DigestAuthenticationOptions()
            {
                Realm = "magical",
                GenerateNonceBytes = () =>
                {
                    var bytes = new byte[16];
                    using (var provider = new RNGCryptoServiceProvider())
                    {
                        provider.GetBytes(bytes);
                    }
                    return bytes;
                }
            };
            app.UseDigestAuthentication(digestOptions);

            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
            app.UseWebApi(config);
        }
    }
}
