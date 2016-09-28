using System.Collections.Generic;
using Newtonsoft.Json;

using VitalsService.Domain.Enums.MessagingHub;

namespace VitalsService.Domain.Dtos.MessagingHub
{
    [JsonObject]
    public class NotificationDto
    {
        public List<string> Tags { get; set; }

        public bool AllTags { get; set; }

        public string Sender { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }

        public RegistrationType[] Types { get; set; }
    }
}
