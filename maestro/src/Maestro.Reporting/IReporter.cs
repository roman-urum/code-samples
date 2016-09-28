using System.IO;

namespace Maestro.Reporting
{
    /// <summary>
    /// IReporter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReporter<in T>
    {
        /// <summary>
        /// Generates the report.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="output">The output.</param>
        void GenerateReport(T dataSource, Stream output);
    }
}