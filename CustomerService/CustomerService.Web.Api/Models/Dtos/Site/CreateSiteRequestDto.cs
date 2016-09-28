using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomerService.Web.Api.Resources;

namespace CustomerService.Web.Api.Models.Dtos.Site
{
    /// <summary>
    /// CreateSiteRequestDto.
    /// </summary>
    /// <seealso cref="CustomerService.Web.Api.Models.Dtos.Site.BaseSiteDto" />
    public class CreateSiteRequestDto : BaseSiteDto
    {
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [StringLength(
            100,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [StringLength(
            50, 
            ErrorMessageResourceType = typeof(GlobalStrings), 
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the Address1.
        /// </summary>
        /// <value>
        /// The Address1.
        /// </value>
        [StringLength(
            250, 
            ErrorMessageResourceType = typeof(GlobalStrings), 
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the Address2.
        /// </summary>
        /// <value>
        /// The Address2.
        /// </value>
        [StringLength(
            250, 
            ErrorMessageResourceType = typeof(GlobalStrings), 
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the Address3.
        /// </summary>
        /// <value>
        /// The Address3.
        /// </value>
        [StringLength(
            250, 
            ErrorMessageResourceType = typeof(GlobalStrings), 
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Address3 { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        [StringLength(
            10, 
            ErrorMessageResourceType = typeof(GlobalStrings), 
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the National Provider Identificator.
        /// </summary>
        /// <value>
        /// The National Provider Identificator.
        /// </value>
        [StringLength(
            100, 
            ErrorMessageResourceType = typeof(GlobalStrings), 
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string NationalProviderIdentificator { get; set; }

        /// <summary>
        /// Gets or sets the customer site identifier.
        /// </summary>
        /// <value>
        /// The customer site identifier.
        /// </value>
        [StringLength(
            100, 
            ErrorMessageResourceType = typeof(GlobalStrings), 
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
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
        /// Gets or sets the categories of care.
        /// </summary>
        /// <value>
        /// The categories of care.
        /// </value>
        public IList<Guid> CategoriesOfCare { get; set; }
    }
}