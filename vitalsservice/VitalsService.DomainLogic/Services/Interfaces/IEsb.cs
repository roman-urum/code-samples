using System.Threading.Tasks;
using VitalsService.Domain.EsbEntities;

namespace VitalsService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IEsb.
    /// </summary>
    public interface IEsb
    {
        /// <summary>
        /// Publishes the measurement.
        /// </summary>
        /// <param name="measurementMessage">The measurement message.</param>
        /// <returns></returns>
        Task PublishMeasurement(MeasurementMessage measurementMessage);

        /// <summary>
        /// Publishes the health session.
        /// </summary>
        /// <param name="healthSession">The health session.</param>
        /// <returns></returns>
        Task PublishHealthSession(object healthSession);
    }
}