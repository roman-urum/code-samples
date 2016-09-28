using MessagingHub.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MessagingHub.Web.Models
{
    public class RegistrationPostRequestModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public IEnumerable<string> Tags { get; set; }

        [Required]
        public RegistrationTypes Type { get; set; }

        [Required]
        [StringLength(250)]
        public string Token { get; set; }

        [Required]
        [StringLength(100)]
        public string Application { get; set; }

        [Required]
        [StringLength(100)]
        public string Device { get; set; }

        [StringLength(256)]
        public string Client { get; set; }
    }
}