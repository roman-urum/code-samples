namespace CustomerService.Web.Api.Models.Dtos.Site
{
    /// <summary>
    /// UpdateCreateSiteRequestDto.
    /// </summary>
    /// <seealso cref="CustomerService.Web.Api.Models.Dtos.Site.BaseSiteDto" />
    public class UpdateSiteRequestDto : CreateSiteRequestDto
    {
        /// <summary>
        /// Gets or sets the is archived.
        /// </summary>
        /// <value>
        /// The is archived.
        /// </value>
        public bool? IsArchived { get; set; }
    }
}