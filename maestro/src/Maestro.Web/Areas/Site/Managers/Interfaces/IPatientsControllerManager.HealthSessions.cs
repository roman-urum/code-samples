using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService;

namespace Maestro.Web.Areas.Site.Managers.Interfaces
{
    /// <summary>
    /// IPatientsControllerManager.HealthSessions
    /// </summary>
    public partial interface IPatientsControllerManager
    {
        /// <summary>
        /// Gets the health sessions.
        /// </summary>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        Task<PagedResult<HealthSessionResponseDto>> GetHealthSessions(SearchHealthSessionsDto searchRequest);
    }
}