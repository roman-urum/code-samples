using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Web.Areas.Site.Models.Dashboard
{
    /// <summary>
    /// GetPatientsCardsRequestViewModel.
    /// </summary>
    /// <seealso cref="Maestro.Domain.Dtos.BaseSearchDto" />
    public class GetPatientsCardsRequestViewModel : BaseSearchDto
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
        /// Gets or sets the caremanager identifier.
        /// </summary>
        /// <value>
        /// The caremanager identifier.
        /// </value>
        public Guid? CareManagerId { get; set; }
    }
}