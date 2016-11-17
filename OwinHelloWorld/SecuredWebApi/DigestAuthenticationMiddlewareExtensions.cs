using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Extensions;
using Owin;

namespace SecuredWebApi
{
    public static class DigestAuthenticationMiddlewareExtensions
    {
        public static IAppBuilder UseDigestAuthentication(this IAppBuilder app, DigestAuthenticationOptions options)
        {
            app.Use(typeof(DigestApplicationMiddleware), options);

            app.UseStageMarker(PipelineStage.Authenticate);

            return app;
        }
    }
}
