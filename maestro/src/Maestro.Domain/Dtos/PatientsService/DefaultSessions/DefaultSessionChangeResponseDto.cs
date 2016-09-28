using Maestro.Domain.Dtos.PatientsService.Calendar;
using Maestro.Domain.Dtos.PatientsService.Enums;

namespace Maestro.Domain.Dtos.PatientsService.DefaultSessions
{
    /// <summary>
    /// Model for response with data about change in default session.
    /// </summary>
    public class DefaultSessionChangeResponseDto : CalendarChangeResponseDto
    {
        /// <summary>
        /// Action performed with health session.
        /// </summary>
        public DefaultSessionAction Action { get; set; }
    }
}
