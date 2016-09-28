using System;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.PatientsService.DefaultSessions;

namespace Maestro.Web.Areas.Site.Managers.Interfaces
{
    /// <summary>
    /// IPatientsControllerManager.DefaultSessions
    /// </summary>
    public partial interface IPatientsControllerManager
    {
        /// <summary>
        /// Removes existing default sessions and creates new.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionDto"></param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateDefaultSession(Guid patientId, DefaultSessionDto defaultSessionDto);

        /// <summary>
        /// Returns default session for specified patient.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task<DefaultSessionResponseDto> GetDefaultSession(Guid patientId);

        /// <summary>
        /// Updates existing session.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionId"></param>
        /// <param name="defaultSessionDto"></param>
        /// <returns></returns>
        Task UpdateDefaultSession(Guid patientId, Guid defaultSessionId, DefaultSessionDto defaultSessionDto);

        /// <summary>
        /// Deletes default session.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionId"></param>
        /// <returns></returns>
        Task DeleteDefaultSession(Guid patientId, Guid defaultSessionId);
    }
}