using System;
using System.Collections.Generic;
using VitalsService.Web.Api.DataAnnotations;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models.PatientConditions
{
    /// <summary>
    /// PatientConditionsRequest.
    /// </summary>
    public class PatientConditionsRequest
    {
        /// <summary>
        /// Gets or sets the patient conditions ids.
        /// </summary>
        /// <value>
        /// The patient conditions ids.
        /// </value>
        [UniqueList(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "UniqueListAttribute_ValidationError",
            ErrorMessage = null
        )]
        public IList<Guid> PatientConditionsIds { get; set; }
    }
}