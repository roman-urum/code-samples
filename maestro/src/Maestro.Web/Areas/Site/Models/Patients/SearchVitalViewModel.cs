using System;
using System.ComponentModel.DataAnnotations;
using Maestro.Domain.Dtos;

namespace Maestro.Web.Areas.Site.Models.Patients
{
    /// <summary>
    /// SearchVitalViewModel.
    /// </summary>
    public class SearchVitalViewModel : BaseSearchDto
    {
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        [Required]
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the observed from.
        /// </summary>
        /// <value>
        /// The observed from.
        /// </value>
        public DateTime? ObservedFrom { get; set; }

        /// <summary>
        /// Gets or sets the observed to.
        /// </summary>
        /// <value>
        /// The observed to.
        /// </value>
        public DateTime? ObservedTo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is invalidated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is invalidated; otherwise, <c>false</c>.
        /// </value>
        public bool IsInvalidated { get; set; }
    }
}