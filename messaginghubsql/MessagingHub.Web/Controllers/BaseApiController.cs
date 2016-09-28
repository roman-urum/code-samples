using MessagingHub.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MessagingHub.Web.Controllers
{
    [ValidationActionFilterAttribute]
    public class BaseApiController : ApiController
    {
        public BaseApiController()
            : this(new MessagingHubDbContext())
        {
            Context = new MessagingHubDbContext();
        }

        public BaseApiController(MessagingHubDbContext context)
            : base()
        { }

        protected MessagingHubDbContext Context { get; private set; }
    }
}
