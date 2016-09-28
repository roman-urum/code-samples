using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessagingHub.Web.Models
{
    public class RegistrationPutRequestModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public IEnumerable<string> Tags { get; set; }

        [Required]
        [StringLength(250)]
        public string Token { get; set; }

        [Required]
        [StringLength(100)]
        public string Application { get; set; }

        [Required]
        [StringLength(100)]
        public string Device { get; set; }

        [Required]
        [StringLength(100)]
        public string Secret { get; set; }

        [StringLength(256)]
        public string Client { get; set; }
    }
}