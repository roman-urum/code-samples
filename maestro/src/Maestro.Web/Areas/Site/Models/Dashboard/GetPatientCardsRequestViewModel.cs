using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Web.Areas.Site.Models.Dashboard
{
    /// <summary>
    /// GetPatientCardsRequestViewModel.
    /// </summary>
    /// <seealso cref="Maestro.Domain.Dtos.BaseSearchDto" />
    public class GetPatientCardsRequestViewModel : BaseSearchDto
    {
        /// <summary>
        /// Gets or sets the list of types.
        /// </summary>
        /// <value>
        /// The list of types.
        /// </value>
        public IList<AlertType> Types { get; set; }

        /// <summary>
        /// Gets or sets the severity dentifiers.
        /// </summary>
        /// <value>
        /// The severity dentifiers.
        /// </value>
        public IList<Guid> SeverityIds { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the caremanager identifier.
        /// </summary>
        /// The caremanager identifier.
        public Guid? CareManagerId { get; set; }
    }
}