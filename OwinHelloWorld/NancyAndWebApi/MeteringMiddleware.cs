using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace NancyAndWebApi
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class MeteringMiddleware
    {
        private readonly AppFunc next;

        public MeteringMiddleware(AppFunc next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            var context = new OwinContext(env);

            var originalStream = context.Response.Body;
            var responseBuffer = new MemoryStream();
            context.Response.Body = responseBuffer;

            await next(env);

            context.Response.ContentLength = responseBuffer.Length;
            Console.WriteLine($"Response body {context.Request.Method} {context.Request.Uri} is {responseBuffer.Length} bytes");

            responseBuffer.Seek(0, SeekOrigin.Begin);

            await responseBuffer.CopyToAsync(originalStream);
        }
    }
}
