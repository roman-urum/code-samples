using System;
using Maestro.Common.ApiClient;
using Maestro.Domain.Dtos.PatientsService.Enums.Ordering;

namespace Maestro.Domain.Dtos.PatientsService.Calendar
{
    /// <summary>
    /// AdherencesSearchDto.
    /// </summary>
    public class AdherencesSearchDto : OrderedSearchDto<AdherenceOrderBy>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdherencesSearchDto"/> class.
        /// </summary>
        public AdherencesSearchDto()
        {
            IsBrief = false;
        }

        /// <summary>
        /// Gets or sets the scheduled before.
        /// </summary>
        /// <value>
        /// The scheduled before.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public DateTime? ScheduledBefore { get; set; }

        /// <summary>
        /// Gets or sets the scheduled after.
        /// </summary>
        /// <value>
        /// The scheduled after.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public DateTime? ScheduledAfter { get; set; }

        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        /// <value>
        /// The event identifier.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public Guid? EventId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include deleted].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include deleted]; otherwise, <c>false</c>.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public bool IncludeDeleted { get; set; }

        /// <summary>
        /// Identifies if response should contains additional data about events.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is brief; otherwise, <c>false</c>.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public bool IsBrief { get; set; }
    }
}
