namespace CustomerService.Web.Api.Models.Dtos.Organizations
{
    /// <summary>
    /// UpdateOrganizationRequestDto.
    /// </summary>
    public class UpdateOrganizationRequestDto : CreateOrganizationRequestDto
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