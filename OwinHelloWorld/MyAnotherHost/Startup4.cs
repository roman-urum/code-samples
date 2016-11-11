using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyAnotherHost.Startup4))]

namespace MyAnotherHost
{
    public class Startup4
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<ResponseReadingMiddleware>();

            app.Run(async context =>
            {
                var bytes = Encoding.UTF8.GetBytes("<h1>****</h1>");
                context.Response.ContentLength = bytes.Length;
                await context.Response.WriteAsync(bytes);
            });
        }
    }
}
