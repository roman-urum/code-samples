using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessagingHub.Data.Models
{
    public class RegistrationTag : Entity
    {
        public Registration Registration { get; set; }

        [Index("UX_RegistrationTagValues", IsUnique = true, Order = 1)]
        public Guid RegistrationId { get; set; }

        [Index("UX_RegistrationTagValues", IsUnique = true, Order = 2)]
        [StringLength(100)]
        public string Value { get; set; }
    }
}