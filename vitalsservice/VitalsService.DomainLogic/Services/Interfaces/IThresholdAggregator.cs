using System.Collections.Generic;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IThresholdAggregator.
    /// </summary>
    public interface IThresholdAggregator
    {
        /// <summary>
        /// Aggregates the thresholds.
        /// </summary>
        /// <param name="defaultThresholds">The default thresholds.</param>
        /// <param name="patientsThresholds">The patients thresholds.</param>
        /// <returns></returns>
        IList<Threshold> AggregateThresholds(IList<DefaultThreshold> defaultThresholds, IList<PatientThreshold> patientsThresholds);
    }
}