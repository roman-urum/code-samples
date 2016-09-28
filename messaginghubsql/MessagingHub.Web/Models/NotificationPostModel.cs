using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MessagingHub.Data.Enums;

namespace MessagingHub.Web.Models
{
    public class NotificationPostModel
    {
        public NotificationPostModel()
        {
            Tags = new List<string>();
            Types = new List<RegistrationTypes>();
        }

        public IEnumerable<string> Tags { get; set; }

        public bool AllTags { get; set; }

        public IEnumerable<RegistrationTypes> Types { get; set; }

        [Required]
        public string Sender { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }

        public int? ZoomId { get; set; }

        //[ZoomStatusValidation]
        public ZoomStatuses? ZoomStatus { get; set; }

        public string CallbackUrl { get; set; }
    }
}