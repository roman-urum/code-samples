using System;

using Maestro.Domain.Dtos;

namespace Maestro.Web.Areas.Site.Models.Patients.SearchPatients
{
    /// <summary>
    /// SearchPatientsViewModel.
    /// </summary>
    /// <seealso cref="Maestro.Domain.Dtos.BaseSearchDto" />
    public class SearchPatientsViewModel : BaseSearchDto
    {
        /// <summary>
        /// Gets or sets the caremanager identifier.
        /// </summary>
        /// <value>The caremanager identifier.</value>
        public Guid? CareManagerId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include inactive patients].
        /// </summary>
        /// <value><c>true</c> if [include inactive patients]; otherwise, <c>false</c>.</value>
        public bool IncludeInactivePatients { get; set; }

        /// <summary>
        /// Gets or sets the identifiers.
        /// </summary>
        /// <value>
        /// The identifiers.
        /// </value>
        public string Identifiers { get; set; }
    }
}