using LightInject;
using Maestro.Reporting.Excel.PatientDetailedData;
using Maestro.Reporting.Excel.PatientTrends;

namespace Maestro.Reporting
{
    /// <summary>
    /// ReportingCompositionRoot.
    /// </summary>
    /// <seealso cref="LightInject.ICompositionRoot" />
    public class ReportingCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Composes services by adding services to the <paramref name="serviceRegistry" />.
        /// </summary>
        /// <param name="serviceRegistry">The target <see cref="T:LightInject.IServiceRegistry" />.</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IPatientTrendsExcelReporter, PatientTrendsExcelReporter>();
            serviceRegistry.Register<IPatientDetailedDataExcelReporter, PatientDetailedDataExcelReporter>();
        }
    }
}