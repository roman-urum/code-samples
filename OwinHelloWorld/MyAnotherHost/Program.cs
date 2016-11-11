using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace MyAnotherHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var controllerType = typeof(MyWebApi.Controllers.EmployeesController);

            using (WebApp.Start<StartupWebApi>("http://localhost:5000"))
            {
                Console.WriteLine("server ready... Press enter to quit");
                Console.ReadKey();
            }
        }
    }
}
