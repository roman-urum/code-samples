using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinHelloWorld
{
    using Owin;
    using System.IO;
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            var middleware = new Func<AppFunc, AppFunc>(Middlware);

            app.Use(middleware);
        }

        public AppFunc Middlware(AppFunc nexdMiddlaware)
        {
            AppFunc appFunc = env =>
            {
                byte[] bytes = Encoding.UTF8.GetBytes("ha ha ha ha :)");
                var headers = (IDictionary<string, string[]>)env["owin.ResponseHeaders"];
                headers["Content-Length"] = new[] { bytes.Length.ToString() };
                headers["Content-Type"] = new[] { "text/plain" };
                var response = (Stream)env["owin.ResponseBody"];
                response.WriteAsync(bytes, 0, bytes.Length);
          
                return nexdMiddlaware(env);
            };
            return appFunc;
        }
    }
}
