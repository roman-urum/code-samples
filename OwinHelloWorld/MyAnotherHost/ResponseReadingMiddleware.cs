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

    public class ResponseReadingMiddleware
    {
        private readonly AppFunc next;

        public ResponseReadingMiddleware(AppFunc next)
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

            responseBuffer.Seek(0, SeekOrigin.Begin);
            string responseBody = await ReadAllAsync(responseBuffer);
            Console.WriteLine(responseBody);

            context.Response.Headers.Add("X-Some-Header", new[] {"Hello"});

            responseBuffer.Seek(0, SeekOrigin.Begin);
            await responseBuffer.CopyToAsync(originalStream);
        }

        private async Task<string> ReadAllAsync(Stream stream)
        {
            string content = null;

            try
            {
                var reader = new StreamReader(stream);
                content = await reader.ReadToEndAsync();

            }
            catch (Exception ex)
            {
                string exception = ex.Message;
            }

            return content;
        }
    }
}
