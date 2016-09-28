using System;
using System.ComponentModel.DataAnnotations;
using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// GetVitalsChartViewModel.
    /// </summary>
    public class GetVitalsChartViewModel
    {
        /// <summary>
        /// Gets or sets the measurement.
        /// </summary>
        /// <value>
        /// The measurement.
        /// </value>
        [EnumDataType(typeof(VitalType))]
        [Required]
        public VitalType Measurement { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        [Required]
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the points per chart.
        /// </summary>
        /// <value>
        /// The points per chart.
        /// </value>
        [Required]
        [Range(1, int.MaxValue)]
        public int PointsPerChart { get; set; }
    }
}