using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyAnotherHost.Startup))]

namespace MyAnotherHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/planets", hellpApp =>
            {
                hellpApp.Map("/3", helloEarth =>
                {
                    helloEarth.Run(async context =>
                    {
                        await context.Response.WriteAsync("<h1>Hello Earth</h1>");
                    });
                });

                hellpApp.MapWhen(context =>
                {
                    if (context.Request.Path.HasValue)
                    {
                        int position;
                        if (int.TryParse(context.Request.Path.Value.Trim('/'), out position))
                        {
                            if (position > 8) return true;
                        }
                    }
                    return false;
                }, helloPluto =>
                {
                    helloPluto.Run(async context =>
                    {
                        await context.Response.WriteAsync("<h1>Opps! We are out of Solar system<h/1>");
                    });
                });

                hellpApp.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("<h1>Hello Mercury1</h1>");
                    await next.Invoke();
                    await context.Response.WriteAsync("<h1>Hello Mercury on return</h1>");
                });
                hellpApp.Run(async context =>
                {
                    await context.Response.WriteAsync("<h1>Hello Neptune</h1>");
                });
            });
            app.Run(async context =>
            {
                await context.Response.WriteAsync("<h1>Hello Universe</h1>");
            });
        }
    }
}
