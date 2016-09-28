using System.Collections.Generic;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.PatientsService.DefaultSessions
{
    /// <summary>
    /// Contains default set of fields for default session.
    /// </summary>
    [JsonObject]
    public class DefaultSessionDto
    {
        /// <summary>
        /// Name of default session.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of protocols scheduled for this session.
        /// </summary>
        public IList<DefaultSessionProtocolDto> Protocols { get; set; }
    }
}