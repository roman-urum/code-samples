using MessagingHub.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessagingHub.Data.Models
{
    public class Application: Entity
    {
        [Required, StringLength(100)]
        [Index("UX_Application", IsUnique = true, Order = 1)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        [Index("UX_Application", IsUnique = true, Order = 2)]
        public string Platform { get; set; }

        public NotificationTypes NotificationType { get; set; }

        public string AppleCertificateBase64 { get; set; }

        [StringLength(255)]
        public string AppleCertificatePassword { get; set; }

        [StringLength(255)]
        public string GoogleCloudMessagingKey { get; set; }

        [StringLength(1024)]
        public string NotificationUrl { get; set; }

        public int? NotificationPort { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
