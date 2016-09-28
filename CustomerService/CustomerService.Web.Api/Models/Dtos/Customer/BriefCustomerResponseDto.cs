using System;
using System.Collections.Generic;

namespace CustomerService.Web.Api.Models.Dtos.Customer
{
    /// <summary>
    /// BriefCustomerResponseDto.
    /// </summary>
    public class BriefCustomerResponseDto : BaseCustomerResponseDto
    {
        /// <summary>
        /// Gets or sets the sites.
        /// </summary>
        public IList<Guid> Sites { get; set; }

        /// <summary>
        /// Gets or sets the organizations.
        /// </summary>
        /// <value>
        /// The organizations.
        /// </value>
        public IList<Guid> Organizations { get; set; }

        /// <summary>
        /// Gets or sets the categories of care.
        /// </summary>
        /// <value>
        /// The categories of care.
        /// </value>
        public IList<Guid> CategoriesOfCare { get; set; }
    }
}