using System.Threading.Tasks;
using AutoMapper;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService;
using Maestro.Web.Areas.Site.Models.Patients;
using Maestro.Web.Areas.Site.Models.Patients.Vitals;

namespace Maestro.Web.Areas.Site.Managers.Implementations
{
    /// <summary>
    /// PatientsControllerManager.Devices
    /// </summary>
    public partial class PatientsControllerManager
    {
        /// <summary>
        /// Searches the vitals.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        public async Task<PagedResult<MeasurementDto>> SearchVitals(SearchVitalViewModel searchModel)
        {
            SearchVitalsDto searchVitalsDto = Mapper.Map<SearchVitalsDto>(searchModel);
            searchVitalsDto.CustomerId = CustomerContext.Current.Customer.Id;
            var token = authDataStorage.GetToken();

            return await this.vitalsService.GetVitals(searchVitalsDto, token);
        }

        /// <summary>
        /// Invalidates measurement
        /// </summary>
        /// <param name="request">The invalidate emasurement request</param>
        /// <returns></returns>
        public async Task InvalidateMeasurement(InvalidateMeasurementViewModel request)
        {
            await vitalsService.InvalidateMeasurement(
                CustomerContext.Current.Customer.Id,
                request.PatientId,
                request.MeasurementId,
                authDataStorage.GetToken());
        }
    }
}