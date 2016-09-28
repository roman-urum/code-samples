using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VitalsService.Domain.Constants;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Enums;

namespace VitalsService.Domain.EsbEntities
{
    public class HealthSessionMessage
    {
        public Guid Id { get; set; }

        /// <summary>
        /// the UTC date/time this health session was received by Maestro
        /// if client supplies this value, it is ignored and set server side on POST 
        /// </summary>
        public DateTime SubmittedUtc { get; set; }

        /// <summary>
        /// GUID of the protocol being taken.
        /// </summary>
        public Guid ProtocolId { get; set; }

        /// <summary>
        /// Name of the protocol being taken
        /// </summary>
        public string ProtocolName { get; set; }

        /// <summary>
        /// Debugging, Testing or Production
        /// </summary>
        public ProcessingType ProcessingType { get; set; }

        /// <summary>
        /// the UTC date/time this health session was scheduled for.
        /// </summary>
        public DateTime ScheduledUtc { get; set; }

        /// <summary>
        /// the calendar item ID that triggered this health session
        /// </summary>
        public Guid CalendarItemId { get; set; }

        /// <summary>
        /// the UTC date/time this health session was started by patient
        /// </summary>
        public DateTime StartedUtc { get; set; }

        /// <summary>
        /// the UTC date/time this health session was completed by patient
        /// </summary>
        public DateTime CompletedUtc { get; set; }

        /// <summary>
        /// the actual elements presented to the patient
        /// </summary>
        public virtual Collection<HealthSessionElementMessage> Elements { get; set; } 
    }
}
