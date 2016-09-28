using System;
using System.Threading.Tasks;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;

namespace VitalsService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// Declares methods to manage measurements and vitals.
    /// </summary>
    public interface IMeasurementsService
    {
        /// <summary>
        /// Creates new measurment with a set of vitals.
        /// </summary>
        /// <param name="measurement"></param>
        /// <param name="rawData">Raw data received from peripheral</param>
        /// <param name="sendMeasurementToEsb">bool flag specifies measurement should be sent to esb or not</param>
        /// <returns>Created entity.</returns>
        Task<Measurement> Create(Measurement measurement, object rawData, bool sendMeasurementToEsb);

        /// <summary>
        /// Updates measurement data.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="measurement">The measurement.</param>
        /// <returns></returns>
        Task<OperationResultDto<Measurement, UpdateMeasurementStatus>> Update(int customerId, Measurement measurement);

        /// <summary>
        /// Returs patients with appropriate search criteria.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="filterMeasurement">The filter.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        Task<PagedResult<Measurement>> Search(
            int customerId,
            Guid patientId, 
            MeasurementsSearchDto filterMeasurement, 
            string clientId = null
        );

        /// <summary>
        /// Returns vital by id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="measurementId">The measurement identifier.</param>
        /// <returns>
        /// Updated entity.
        /// </returns>
        Task<Measurement> GetById(int customerId, Guid patientId, Guid measurementId);
    }
}
