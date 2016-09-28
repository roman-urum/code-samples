using System.Collections.Generic;
using CustomerService.Web.Api.Models.Dtos.CategoryOfCare;
using CustomerService.Web.Api.Models.Dtos.Organizations;
using CustomerService.Web.Api.Models.Dtos.Site;

namespace CustomerService.Web.Api.Models.Dtos.Customer
{
    /// <summary>
    /// FullCustomerResponseDto.
    /// </summary>
    public class FullCustomerResponseDto : BaseCustomerResponseDto
    {
        /// <summary>
        /// Gets or sets the sites.
        /// </summary>
        public IList<SiteResponseDto> Sites { get; set; }

        /// <summary>
        /// Gets or sets the organizations.
        /// </summary>
        /// <value>
        /// The organizations.
        /// </value>
        public IList<OrganizationResponseDto> Organizations { get; set; }

        /// <summary>
        /// Gets or sets the categories of care.
        /// </summary>
        /// <value>
        /// The categories of care.
        /// </value>
        public IList<CategoryOfCareResponseDto> CategoriesOfCare { get; set; }
    }
}