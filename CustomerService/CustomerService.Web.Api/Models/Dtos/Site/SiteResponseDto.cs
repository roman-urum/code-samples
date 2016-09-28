using System;
using System.Collections.Generic;
using CustomerService.Web.Api.Models.Dtos.CategoryOfCare;

namespace CustomerService.Web.Api.Models.Dtos.Site
{
    /// <summary>
    /// Dto for responses of Sites API.
    /// </summary>
    public class SiteResponseDto : BaseSiteDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the Address1.
        /// </summary>
        /// <value>
        /// The Address1.
        /// </value>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the Address2.
        /// </summary>
        /// <value>
        /// The Address2.
        /// </value>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the Address3.
        /// </summary>
        /// <value>
        /// The Address3.
        /// </value>
        public string Address3 { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the National Provider Identificator.
        /// </summary>
        /// <value>
        /// The National Provider Identificator.
        /// </value>
        public string NationalProviderIdentificator { get; set; }

        /// <summary>
        /// Gets or sets the customer site identifier.
        /// </summary>
        /// <value>
        /// The customer site identifier.
        /// </value>
        public string CustomerSiteId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this site is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this site is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is published.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is published; otherwise, <c>false</c>.
        /// </value>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is archived.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is archived; otherwise, <c>false</c>.
        /// </value>
        public bool IsArchived { get; set; }

        /// <summary>
        /// Gets or sets the categories of care.
        /// </summary>
        /// <value>
        /// The categories of care.
        /// </value>
        public IList<CategoryOfCareResponseDto> CategoriesOfCare { get; set; }
    }
}