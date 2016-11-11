using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyAnotherHost.StartupWebApi))]

namespace MyAnotherHost
{
    public class StartupWebApi
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}");

            app.UseWebApi(config);

            app.Run(async context =>
            {
                var bytes = Encoding.UTF8.GetBytes("<h1>Hello World</h1>");
                context.Response.ContentLength = bytes.Length;
                await context.Response.WriteAsync(bytes);
            });
        }
    }
}
