using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MessagingHub.Data.Enums;

namespace MessagingHub.Data.Models
{
    public class Registration : Entity
    {
        public Registration()
        {
            Tags = new List<RegistrationTag>();
        }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Secret { get; set; }

        public ICollection<RegistrationTag> Tags { get; set; }

        [Index("UX_RegistrationTypeToken", IsUnique = true, Order = 1)]
        public RegistrationTypes Type { get; set; }

        [Index("UX_RegistrationTypeToken", IsUnique = true, Order = 2)]
        [StringLength(250)]
        public string Token { get; set; }

        public Application Application { get; set; }

        public Guid? ApplicationId { get; set; }

        [StringLength(100)]
        public string Device { get; set; }

        [Index]
        public bool Verified { get; set; }

        [StringLength(10)]
        public string VerificationCode { get; set; }

        [Index]
        public bool Disabled { get; set; }

        [StringLength(256)]
        public string Client { get; set; }
    }
}
