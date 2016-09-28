using System;
using System.ComponentModel.DataAnnotations;

using Maestro.Web.Resources;

namespace Maestro.Web.Areas.Site.Models.Patients.Vitals
{
    /// <summary>
    ///Model for invalidating measurement. 
    /// </summary>
    public class InvalidateMeasurementViewModel
    {
        /// <summary>
        /// The patient identifier
        /// </summary>
        [Required(
            ErrorMessage = null,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError")]
        public Guid PatientId { get; set; }

        /// <summary>
        /// The measurement identifier to be invalidated
        /// </summary>
        [Required(
            ErrorMessage = null,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError")]
        public Guid MeasurementId { get; set; }
    }
}