using System;

namespace CustomerService.Domain.Dtos
{
    /// <summary>
    /// SiteSearchDto.
    /// </summary>
    public class SiteSearchDto : BaseSearchDto
    {
        /// <summary>
        /// Gets or sets a value indicating whether [include archived].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include archived]; otherwise, <c>false</c>.
        /// </value>
        public bool IncludeArchived { get; set; }

        /// <summary>
        /// Gets or sets the organization identifier.
        /// </summary>
        /// <value>
        /// The organization identifier.
        /// </value>
        public Guid? OrganizationId { get; set; }
    }
}