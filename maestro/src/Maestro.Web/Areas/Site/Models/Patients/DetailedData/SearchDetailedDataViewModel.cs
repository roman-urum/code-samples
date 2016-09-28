using System;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Web.Areas.Site.Models.Patients.DetailedData
{
    /// <summary>
    /// SearchDetailedDataViewModel.
    /// </summary>
    public class SearchDetailedDataViewModel : BaseSearchDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchDetailedDataViewModel"/> class.
        /// </summary>
        public SearchDetailedDataViewModel()
        {
            var now = DateTime.UtcNow;

            ObservedFromUtc = now.AddDays(-30);
            ObservedToUtc = now;
        }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the observed from.
        /// </summary>
        /// <value>
        /// The observed from.
        /// </value>
        public DateTime? ObservedFromUtc { get; set; }

        /// <summary>
        /// Gets or sets the observed to.
        /// </summary>
        /// <value>
        /// The observed to.
        /// </value>
        public DateTime? ObservedToUtc { get; set; }

        /// <summary>
        /// Gets or sets the health session element type
        /// </summary>
        /// <value>
        /// The health session element type
        /// </value>
        public HealthSessionElementType? ElementType { get; set; }
    }
}