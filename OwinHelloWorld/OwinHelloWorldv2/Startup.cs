using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(OwinHelloWorldv2.Startup))]

namespace OwinHelloWorldv2
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("<h1>pam param</h1>");

                await next.Invoke();

                await context.Response.WriteAsync("<h1>pam param end</h1>");
           
            });

            app.Run(context =>
            {
                byte[] bytes = Encoding.UTF8.GetBytes("<h2>ha ha ha ha :) v2</h2>");
                var response = context.Response;
                return response.WriteAsync(bytes);
            });
        }
    }
}
