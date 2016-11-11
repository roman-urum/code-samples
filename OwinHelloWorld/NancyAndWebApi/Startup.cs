using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Nancy.Owin;
using Owin;

[assembly: OwinStartup(typeof(NancyAndWebApi.Startup))]

namespace NancyAndWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<MeteringMiddleware>();

            //Doesn't work, don't know why
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileSystem = new PhysicalFileSystem(@"C:\code-samples"),
                RequestPath = new PathString("/files")
            });

            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}");

            app.UseWebApi(config);

            app.UseNancy();
        }
    }
}
