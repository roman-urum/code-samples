using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VitalsService.Domain.Enums;

namespace VitalsService.Domain.EsbEntities
{
    public class HealthSessionElementValueMessage
    {
        public Guid HealthSessionElementId { get; set; }

        /// <summary>
        /// Type of answer.
        /// </summary>
        public HealthSessionElementValueType Type { get; set; }
    }
}
