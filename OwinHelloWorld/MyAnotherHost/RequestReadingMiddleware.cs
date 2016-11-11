using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace MyAnotherHost
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class RequestReadingMiddleware
    {
        private readonly AppFunc next;

        public RequestReadingMiddleware(AppFunc next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            var context = new OwinContext(env);

            var requestBuffer = new MemoryStream();
            await context.Request.Body.CopyToAsync(requestBuffer);
            requestBuffer.Seek(0, SeekOrigin.Begin);

            context.Request.Body = requestBuffer;

            var reader = new StreamReader(context.Request.Body);
            string content = await reader.ReadToEndAsync();

            ((MemoryStream) context.Request.Body).Seek(0, SeekOrigin.Begin);

            Console.WriteLine(content);

            await this.next(env);
        }
    }
}
