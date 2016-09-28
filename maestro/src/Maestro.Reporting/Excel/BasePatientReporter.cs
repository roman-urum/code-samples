using System;
using System.Linq;
using Maestro.Common.Extensions;
using Maestro.Domain.Dtos.PatientsService;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Maestro.Reporting.Excel
{
    /// <summary>
    /// BasePatientReporter.
    /// </summary>
    public abstract class BasePatientReporter
    {
        /// <summary>
        /// Draws the patient header.
        /// </summary>
        /// <param name="ws">The ws.</param>
        /// <param name="patientInfo">The patient information.</param>
        /// <param name="currentRow">The current row.</param>
        protected void DrawPatientHeader(ExcelWorksheet ws, PatientDto patientInfo, ref int currentRow)
        {
            ws.Cells[currentRow, 1].Value = "Patient Name";
            ws.Cells[currentRow, 1].Style.Font.Bold = true;
            ws.Cells[currentRow, 2].Value =
                string.Format("{0} {1}", patientInfo.FirstName, patientInfo.LastName);

            currentRow++;

            ws.Cells[currentRow, 1].Value = "Patient ID";
            ws.Cells[currentRow, 1].Style.Font.Bold = true;

            if (patientInfo.Identifiers.Any())
            {
                foreach (var identifier in patientInfo.Identifiers.OrderBy(i => i.Name))
                {
                    ws.Cells[currentRow, 2].Value = identifier.Name;
                    ws.Cells[currentRow, 3].Value = identifier.Value;

                    currentRow++;
                }

                ws.Cells[2, 1, patientInfo.Identifiers.Count + 1, 1].Merge = true;
                ws.Cells[2, 1, patientInfo.Identifiers.Count + 1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            }
            else
            {
                currentRow++;
            }

            ws.Cells[currentRow, 1].Value = "Age";
            ws.Cells[currentRow, 1].Style.Font.Bold = true;
            ws.Cells[currentRow, 2].Value = 
                DateTime.Parse(patientInfo.BirthDate).YearsSince().ToString();

            currentRow++;

            ws.Cells[currentRow, 1].Value = "DOB";
            ws.Cells[currentRow, 1].Style.Font.Bold = true;
            ws.Cells[currentRow, 2].Value = patientInfo.BirthDate;

            currentRow++;

            ws.Cells[currentRow, 1].Value = "Gender";
            ws.Cells[currentRow, 1].Style.Font.Bold = true;
            ws.Cells[currentRow, 2].Value = patientInfo.Gender;

            currentRow++;

            ws.Cells[currentRow, 1].Value = "Exported";
            ws.Cells[currentRow, 1].Style.Font.Bold = true;
            ws.Cells[currentRow, 2].Value = 
                DateTime.UtcNow.ConvertTimeFromUtc(patientInfo.TimeZone).ToString("yyyy/M/d h:mm:ss tt");

            currentRow++;
            currentRow++;
        }
    }
}