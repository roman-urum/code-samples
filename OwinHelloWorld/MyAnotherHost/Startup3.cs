using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyAnotherHost.Startup3))]

namespace MyAnotherHost
{
    public class Startup3
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<RequestReadingMiddleware>();

            app.Run(async context =>
            {
                string body = string.Empty;
                using (var reader = new StreamReader(context.Request.Body))
                {
                    body = await reader.ReadToEndAsync();
                }                
                context.Response.ContentLength = Encoding.UTF8.GetByteCount(body);
                await context.Response.WriteAsync(body);
            });
        }
    }
}
