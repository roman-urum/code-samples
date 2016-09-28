using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos.VitalsService.AlertSeverities;

namespace Maestro.Web.Areas.Customer.Managers.Implementations
{
    public partial class CareBuilderControllerManager
    {
        /// <summary>
        /// Returns alert severities for current customer.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<AlertSeverityResponseDto>> GetAlertSeverities()
        {
            var token = authDataStorage.GetToken();
            var resuts = await this.vitalsService.GetAlertSeverities(CustomerContext.Current.Customer.Id, token);

            return resuts;
        }
    }
}