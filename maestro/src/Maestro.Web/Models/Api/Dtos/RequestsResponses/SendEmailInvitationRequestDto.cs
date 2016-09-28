using System.ComponentModel.DataAnnotations;
using Maestro.Web.Resources;

namespace Maestro.Web.Models.Api.Dtos.RequestsResponses
{
    /// <summary>
    /// SendEmailInvitationRequestDto.
    /// </summary>
    public class SendEmailInvitationRequestDto
    {
        [Required(ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "RequiredAttribute_ValidationError")]
        [EmailAddress(ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "EmailAddressAttribute_Invalid")]
        [StringLength(256, ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string Email { get; set; }
    }
}