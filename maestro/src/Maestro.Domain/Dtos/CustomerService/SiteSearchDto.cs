using System;
using Maestro.Common.ApiClient;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.CustomerService
{
    /// <summary>
    /// SiteSearchDto.
    /// </summary>
    [JsonObject]
    public class SiteSearchDto : BaseSearchDto
    {
        /// <summary>
        /// Gets or sets a value indicating whether [include archived].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include archived]; otherwise, <c>false</c>.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public bool IncludeArchived { get; set; }

        /// <summary>
        /// Gets or sets the organization identifier.
        /// </summary>
        /// <value>
        /// The organization identifier.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public Guid? OrganizationId { get; set; }
    }
}