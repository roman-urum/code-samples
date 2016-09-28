using System;

namespace Maestro.Domain.Dtos.PatientsService.DefaultSessions
{
    /// <summary>
    /// Dto used for responses with default session data.
    /// Includes id to default session dto.
    /// </summary>
    public class DefaultSessionResponseDto : DefaultSessionDto
    {
        /// <summary>
        /// Unique id of default session.
        /// </summary>
        public Guid Id { get; set; }
    }
}
