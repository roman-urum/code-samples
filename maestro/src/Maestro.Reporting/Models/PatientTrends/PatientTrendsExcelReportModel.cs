using System.Collections.Generic;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.Domain.Dtos.VitalsService.Thresholds;

namespace Maestro.Reporting.Models.PatientTrends
{
    /// <summary>
    /// PatientTrendsExcelReportModel.
    /// </summary>
    public class PatientTrendsExcelReportModel
    {
        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        /// <value>
        /// The patient.
        /// </value>
        public PatientDto Patient { get; set; }

        /// <summary>
        /// Gets or sets the vital charts data.
        /// </summary>
        /// <value>
        /// The vital charts data.
        /// </value>
        public IList<VitalResponseModel> VitalsData { get; set; }

        /// <summary>
        /// Gets or sets the assesment charts data.
        /// </summary>
        /// <value>
        /// The assesment charts data.
        /// </value>
        public IList<QuestionAnswersModel> QuestionsData { get; set; }

        /// <summary>
        /// Gets or sets the thresholds.
        /// </summary>
        /// <value>
        /// The thresholds.
        /// </value>
        public IList<BaseThresholdDto> Thresholds { get; set; }
    }
}