using MessagingHub.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessagingHub.Web.Models
{
    public class RegistrationPostResponseModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Secret { get; set; }

        //public ICollection<string> Tags { get; set; }

        //public RegistrationTypes Type { get; set; }

        //public string Token { get; set; }

        //public string Application { get; set; }

        //public string Device { get; set; }
    }
}