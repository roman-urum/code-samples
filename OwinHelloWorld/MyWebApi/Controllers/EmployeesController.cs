﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    public class EmployeesController: ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            //return Request.CreateResponse(new Employee()
            //{
            //    Id = id,
            //    FirstName = "Johnny",
            //    LastName = "Law"
            //});

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}