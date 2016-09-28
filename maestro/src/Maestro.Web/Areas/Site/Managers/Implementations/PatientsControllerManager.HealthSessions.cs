using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService;

namespace Maestro.Web.Areas.Site.Managers.Implementations
{
    /// <summary>
    /// PatientsControllerManager.HealthSessions
    /// </summary>
    public partial class PatientsControllerManager
    {
        /// <summary>
        /// Gets the health sessions.
        /// </summary>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public async Task<PagedResult<HealthSessionResponseDto>> GetHealthSessions(
            SearchHealthSessionsDto searchRequest
        )
        {
            var token = authDataStorage.GetToken();
            searchRequest.CustomerId = CustomerContext.Current.Customer.Id;

            return await vitalsService.GetHealthSessions(searchRequest, token);
        }
    }
}