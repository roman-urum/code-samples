using System;
using System.Threading.Tasks;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService.AlertSeverities;

namespace Maestro.DataAccess.Api.DataProviders.Implementations
{
    /// <summary>
    /// VitalsDataProvider.
    /// </summary>
    public partial class VitalsDataProvider : IVitalsDataProvider
    {
        /// <summary>
        /// Returns alert severities for specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<PagedResult<AlertSeverityResponseDto>> GetAlertSeverities(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}