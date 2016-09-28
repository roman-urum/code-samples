using System.Collections.Generic;
using System.IO;
using System.Linq;
using Maestro.Common.Extensions;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.Domain.Dtos.VitalsService.Thresholds;
using Maestro.Reporting.Models.PatientTrends;
using OfficeOpenXml;

namespace Maestro.Reporting.Excel.PatientTrends
{
    /// <summary>
    /// PatientTrendsReporter.
    /// </summary>
    /// <seealso cref="IPatientTrendsExcelReporter" />
    public class PatientTrendsExcelReporter : BasePatientReporter, IPatientTrendsExcelReporter
    {
        /// <summary>
        /// Generates the report.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="output">The output.</param>
        public void GenerateReport(PatientTrendsExcelReportModel dataSource, Stream output)
        {
            using (var pck = new ExcelPackage())
            {
                if (dataSource.QuestionsData.Any() || dataSource.VitalsData.Any())
                {
                    GenerateVitalsWorksheets(pck, dataSource.Patient, dataSource.VitalsData, dataSource.Thresholds);

                    GenerateQuestionsWorksheets(pck, dataSource.Patient, dataSource.QuestionsData);
                }
                else
                {
                    pck.Workbook.Worksheets.Add("No data");
                }

                pck.SaveAs(output);
            }
        }

        private void GenerateVitalsWorksheets(
            ExcelPackage pck, 
            PatientDto patient,
            IList<VitalResponseModel> vitalsData,
            IList<BaseThresholdDto> thresholds
        )
        {
            var groupedVitals = vitalsData
                .GroupBy(v => new { v.Name, v.Unit })
                .ToList();

            foreach (var group in groupedVitals)
            {
                var currentRow = 1;

                var vitalName = group.Key.Name.Description();
                var vitalUnit = group.Key.Unit.Description();

                var ws = pck.Workbook.Worksheets.Add(group.Key.Name.Description());

                DrawPatientHeader(ws, patient, ref currentRow);

                var patientVitalSpecificThresholds = thresholds.Where(t => t.Name == group.Key.Name).ToList();
                var patientVitalSpecificThresholdsOrderedByAlertSeverityAsc =
                    patientVitalSpecificThresholds.OrderBy(t => t.AlertSeverity.Severity).ToList();
                var patientVitalSpecificThresholdsOrderedByAlertSeverityDesc =
                    patientVitalSpecificThresholds.OrderByDescending(t => t.AlertSeverity.Severity).ToList();

                // Low/High labels
                for (int i = 0; i < patientVitalSpecificThresholds.Count; i++)
                {
                    ws.Cells[currentRow, i + 4].Value = "Low";
                    ws.Cells[currentRow, i + 4].Style.Font.Bold = true;
                    ws.Cells[currentRow, i + 4 + patientVitalSpecificThresholds.Count].Value = "High";
                    ws.Cells[currentRow, i + 4 + patientVitalSpecificThresholds.Count].Style.Font.Bold = true;
                }

                currentRow++;

                // Table head
                ws.Cells[currentRow, 1].Value = "Date";
                ws.Cells[currentRow, 1].Style.Font.Bold = true;

                ws.Cells[currentRow, 2].Value = "Time";
                ws.Cells[currentRow, 2].Style.Font.Bold = true;

                ws.Cells[currentRow, 3].Value =
                    string.Format("{0}, {1}", vitalName, vitalUnit);
                ws.Cells[currentRow, 3].Style.Font.Bold = true;

                for (int i = 0; i < patientVitalSpecificThresholds.Count; i++)
                {
                    ws.Cells[currentRow, i + 4].Value =
                        string.Format(
                            "{0}, {1}",
                            patientVitalSpecificThresholdsOrderedByAlertSeverityDesc[i].AlertSeverity.Name,
                            vitalUnit
                        );
                    ws.Cells[currentRow, i + 4].Style.Font.Bold = true;
                    ws.Cells[currentRow, i + 4 + patientVitalSpecificThresholds.Count].Value =
                        string.Format(
                            "{0}, {1}",
                            patientVitalSpecificThresholdsOrderedByAlertSeverityAsc[i].AlertSeverity.Name,
                            vitalUnit
                        );
                    ws.Cells[currentRow, i + 4 + patientVitalSpecificThresholds.Count].Style.Font.Bold = true;
                }

                currentRow++;

                // Vitals
                foreach (var vital in group)
                {
                    ws.Cells[currentRow, 1].Value =
                        vital.Observed.ToString("yyyy/M/d");
                    ws.Cells[currentRow, 2].Value =
                        vital.Observed.ToString("h:mm tt (zzz)");
                    ws.Cells[currentRow, 3].Value = vital.Value;

                    for (int i = 0; i < patientVitalSpecificThresholds.Count; i++)
                    {
                        ws.Cells[currentRow, i + 4].Value =
                            patientVitalSpecificThresholdsOrderedByAlertSeverityDesc[i].MinValue;
                        ws.Cells[currentRow, i + 4 + patientVitalSpecificThresholds.Count].Value =
                            patientVitalSpecificThresholdsOrderedByAlertSeverityAsc[i].MaxValue;
                    }

                    currentRow++;
                }

                // Columns width
                ws.Cells.AutoFitColumns();
            }
        }

        private void GenerateQuestionsWorksheets(
            ExcelPackage pck,
            PatientDto patient,
            IList<QuestionAnswersModel> questionsData
        )
        {
            foreach (var question in questionsData)
            {
                var currentRow = 1;

                var ws = pck.Workbook.Worksheets.Add(string.Format("Question {0}", questionsData.IndexOf(question) + 1));

                DrawPatientHeader(ws, patient, ref currentRow);

                currentRow++;

                ws.Cells[currentRow, 1].Value = "Question";
                ws.Cells[currentRow, 1].Style.Font.Bold = true;
                ws.Cells[currentRow, 2].Value = question.Question;

                currentRow++;

                // Table head
                ws.Cells[currentRow, 1].Value = "Date";
                ws.Cells[currentRow, 1].Style.Font.Bold = true;

                ws.Cells[currentRow, 2].Value = "Time";
                ws.Cells[currentRow, 2].Style.Font.Bold = true;
                ws.Cells[currentRow, 3].Value = "Answer";
                ws.Cells[currentRow, 3].Style.Font.Bold = true;

                currentRow++;

                // Answers
                foreach (var answer in question.Answers)
                {
                    ws.Cells[currentRow, 1].Value = answer.Answered.ToString("yyyy/M/d");
                    ws.Cells[currentRow, 2].Value = answer.Answered.ToString("h:mm tt (zzz)");
                    ws.Cells[currentRow, 3].Value = answer.Text;

                    currentRow++;
                }

                // Columns width
                ws.Cells.AutoFitColumns();
            }
        }
    }
}