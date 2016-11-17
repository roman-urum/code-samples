﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SecuredWebApi
{
    public class EmployeesController: ApiController
    {
        [Authorize]
        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse("Hello Employee");
        }
    }
}
