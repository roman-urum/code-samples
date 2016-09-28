using System;
using System.Threading.Tasks;
using Maestro.Domain.DbEntities;

namespace Maestro.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// Contains business logic for saving of trends settings in db.
    /// </summary>
    public interface ITrendsSettingsService
    {
        /// <summary>
        /// Deletes all existing charts settings for patient with the same id and create provided entity.
        /// </summary>
        /// <param name="trendSetting"></param>
        /// <returns></returns>
        Task Save(TrendSetting trendSetting);

        /// <summary>
        /// Returns charts settings for specified patient.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task<TrendSetting> GetByPatientId(Guid patientId);
    }
}
