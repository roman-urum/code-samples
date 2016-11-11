using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace MyAnotherHost
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class MachineNameMidleware
    {
        private readonly AppFunc next;

        public MachineNameMidleware(AppFunc next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {            
            var context = new OwinContext(env);

            context.Response.OnSendingHeaders(state =>
            {
                var response = (OwinResponse) state;
                if (response.StatusCode > 400)
                {
                    response.Headers.Add("X-Box", new[] { Environment.MachineName });
                    response.Headers.Add("haha-header", new[] { ":)" });
                }
            }, context.Response);


            await this.next(env);
        }
    }
}
