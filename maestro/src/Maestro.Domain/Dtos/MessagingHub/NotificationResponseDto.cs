using System;
using System.Collections.Generic;

namespace Maestro.Domain.Dtos.MessagingHub
{
    public class NotificationResponseDto
    {
        public List<Guid> Notified { get; set; } 

        public List<Guid> NotNotified { get; set; } 
    }
}
