using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos.VitalsService.AlertSeverities;

namespace Maestro.Web.Areas.Customer.Managers.Interfaces
{
    public partial interface ICareBuilderControllerManager
    {
        /// <summary>
        /// Returns alert severities for current customer.
        /// </summary>
        /// <returns></returns>
        Task<IList<AlertSeverityResponseDto>> GetAlertSeverities();
    }
}