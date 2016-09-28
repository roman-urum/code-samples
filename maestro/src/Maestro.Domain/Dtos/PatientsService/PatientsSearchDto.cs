using System;
using System.Collections.Generic;
using Maestro.Common.ApiClient;
using Maestro.Domain.Dtos.PatientsService.Enums.Ordering;

namespace Maestro.Domain.Dtos.PatientsService
{
    /// <summary>
    /// Class SearchPatientsDto.
    /// </summary>
    public class PatientsSearchDto : OrderedSearchDto<PatientOrderBy>
    {
        public PatientsSearchDto()
        {
            this.Statuses = new List<PatientStatus>();
        }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>The customer identifier.</value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>The site identifier.</value>
        [RequestParameter(RequestParameterType.QueryString)]
        public Guid? SiteId { get; set; }

        /// <summary>
        /// Gets or sets the care manager identifier.
        /// </summary>
        /// <value>The care manager identifier.</value>
        [RequestParameter(RequestParameterType.QueryString)]
        public Guid? CareManagerId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is brief.
        /// </summary>
        /// <value><c>true</c> if this instance is brief; otherwise, <c>false</c>.</value>
        [RequestParameter(RequestParameterType.QueryString)]
        public bool IsBrief { get; set; }

        /// <summary>
        /// Patients statuses to filter patients list by patient status.
        /// Returns all if list is empty.
        /// </summary>
        [RequestParameter(RequestParameterType.QueryString)]
        public IList<PatientStatus> Statuses { get; set; }

        /// <summary>
        /// Gets or sets the identifiers.
        /// </summary>
        /// <value>
        /// The identifiers.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public string Identifiers { get; set; }
    }
}
