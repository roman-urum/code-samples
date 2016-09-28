using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalsService.Domain.EsbEntities
{
    public class DeviceMessage
    {
        public string UniqueIdentifier { get; set; }
        public decimal? BatteryPercent { get; set; }
        public int? BatteryMillivolts { get; set; }
        public string Model { get; set; }
        public string Version { get; set; }
    }
}
