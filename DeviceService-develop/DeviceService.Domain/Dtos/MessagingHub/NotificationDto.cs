using System.Collections.Generic;
using DeviceService.Domain.Entities.Enums;
using Newtonsoft.Json;

namespace DeviceService.Domain.Dtos.MessagingHub
{
    [JsonObject]
    public class NotificationDto
    {
        public IList<string> Tags { get; set; }

        public bool AllTags { get; set; }

        public string Sender { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }

        public IList<RegistrationType> Types { get; set; }
    }
}