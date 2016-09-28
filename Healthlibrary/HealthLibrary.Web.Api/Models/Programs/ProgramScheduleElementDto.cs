using System;

namespace HealthLibrary.Web.Api.Models.Programs
{
    /// <summary>
    /// Model for protocol element in program schedule response.
    /// </summary>
    public class ProgramScheduleElementDto
    {
        /// <summary>
        /// Id of protocol.
        /// </summary>
        public Guid ProtocolId { get; set; }

        /// <summary>
        /// Protocol order for specified day or global.
        /// </summary>
        public int Order { get; set; }
    }
}