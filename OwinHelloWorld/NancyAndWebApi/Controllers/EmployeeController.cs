using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace NancyAndWebApi.Controllers
{
    public class EmployeeController: ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse("Hello Employee");
        }
    }
}
