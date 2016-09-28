using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.TokenService.RequestsResponses
{
    /// <summary>
    /// Model for request to update existing principal.
    /// </summary>
    [JsonObject]
    public class UpdatePrincipalModel : BasePrincipalModel
    {
        /// <summary>
        /// New credential.
        /// </summary>
        public CredentialUpdateModel Credential { get; set; }
    }
}
