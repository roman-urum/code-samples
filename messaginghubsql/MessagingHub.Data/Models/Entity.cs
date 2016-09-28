using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingHub.Data.Models
{
    public class Entity
    {
        public Entity()
        {
            Id = SequentialGuid.GenerateComb();
        }

        [Key]
        public Guid Id { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
