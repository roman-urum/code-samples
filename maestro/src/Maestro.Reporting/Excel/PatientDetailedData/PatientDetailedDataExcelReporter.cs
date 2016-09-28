using System.IO;
using Maestro.Reporting.Models.PatientDetailedData;
using OfficeOpenXml;

namespace Maestro.Reporting.Excel.PatientDetailedData
{
    /// <summary>
    /// PatientTrendsReporter.
    /// </summary>
    /// <seealso cref="IPatientDetailedDataExcelReporter" />
    public class PatientDetailedDataExcelReporter : BasePatientReporter, IPatientDetailedDataExcelReporter
    {
        /// <summary>
        /// Generates the report.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="output">The output.</param>
        public void GenerateReport(PatientDetailedDataExcelReportModel dataSource, Stream output)
        {
            using (var pck = new ExcelPackage())
            {
                var currentRow = 1;

                var ws = pck.Workbook.Worksheets.Add("Detailed data");
                ws.OutLineSummaryBelow = false;

                DrawPatientHeader(ws, dataSource.Patient, ref currentRow);

                ws.Cells[currentRow, 1].Value = "Date";
                ws.Cells[currentRow, 1].Style.Font.Bold = true;

                ws.Cells[currentRow, 2].Value = "Time";
                ws.Cells[currentRow, 2].Style.Font.Bold = true;

                currentRow++;

                foreach (var reportElement in dataSource.Elements)
                {
                    ws.Cells[currentRow, 1].Value = reportElement.Date.HasValue ? 
                        reportElement.Date.Value.ToString("yyyy/M/d") :
                        string.Empty;
                    ws.Cells[currentRow, 2].Value = reportElement.Date.HasValue ?
                        reportElement.Date.Value.ToString("h:mm tt (zzz)") :
                        string.Empty;
                    ws.Cells[currentRow, 3].Value = reportElement.Description;

                    var model = reportElement as GroupedDetailedDataRowModel;

                    if (model != null)
                    {
                        ws.Cells[currentRow, 1].Style.Font.Bold = true;
                        ws.Cells[currentRow, 2].Style.Font.Bold = true;
                        ws.Cells[currentRow, 3].Style.Font.Bold = true;

                        foreach (var subElement in model.GroupElements)
                        {
                            currentRow++;

                            ws.Cells[currentRow, 1].Value = subElement.Date.HasValue ?
                                subElement.Date.Value.ToString("yyyy/M/d") :
                                string.Empty;
                            ws.Cells[currentRow, 1].Style.Indent = 1;
                            ws.Cells[currentRow, 2].Value = subElement.Date.HasValue ?
                                subElement.Date.Value.ToString("h:mm tt (zzz)") :
                                string.Empty;
                            ws.Cells[currentRow, 3].Value = subElement.Description;
                            ws.Cells[currentRow, 4].Value = subElement.Value;

                            ws.Row(currentRow).OutlineLevel = 1;
                        }
                    }
                    else
                    {
                        ws.Cells[currentRow, 4].Value = reportElement.Value;
                    }

                    currentRow++;
                }

                // Columns width
                ws.Cells.AutoFitColumns();

                pck.SaveAs(output);
            }
        }
    }
}