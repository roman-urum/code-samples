using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace NancyAndWebApi
{
    public class HomeModule: NancyModule
    {
        public HomeModule()
        {
            Get["/"] = x => "<h1>Hello World!<h1>";
            Get["/test"] = x => "<h2>TEST!<h2>";
        }
    }
}
