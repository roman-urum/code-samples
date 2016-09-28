using System.Collections.Generic;
using Maestro.Domain.Dtos.PatientsService;

namespace Maestro.Reporting.Models.PatientDetailedData
{
    /// <summary>
    /// PatientDetailedDataExcelReportModel.
    /// </summary>
    public class PatientDetailedDataExcelReportModel
    {
        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        /// <value>
        /// The patient.
        /// </value>
        public PatientDto Patient { get; set; }

        /// <summary>
        /// Gets or sets the elements of the report.
        /// </summary>
        /// <value>
        /// The elements of the report.
        /// </value>
        public IList<DetailedDataRowModel> Elements { get; set; }
    }
}