using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyAnotherHost.Startup2))]

namespace MyAnotherHost
{
    public class Startup2
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<MachineNameMidleware>();

            app.Use(async (context, next) =>
            {
                context.Response.StatusCode = 404;                
                await next.Invoke();
            });

            app.Run(async context =>
            {
                var bytes = Encoding.UTF8.GetBytes("<h1>Hello World</h1>");
                var moreBytes = Encoding.UTF8.GetBytes("<h1>Hello Universe</h1>");
                var moreBytes2 = Encoding.UTF8.GetBytes("<h1>Hello</h1>");

                context.Response.ContentLength = bytes.Length + moreBytes.Length + moreBytes2.Length;

                await context.Response.WriteAsync(bytes);
                await context.Response.WriteAsync(moreBytes);
                await context.Response.WriteAsync(moreBytes2);
                
            });
        }
    }
}
