using System.Collections.Generic;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.TokenService.RequestsResponses
{
    /// <summary>
    /// Model for request to create new principal.
    /// </summary>
    [JsonObject]
    public class CreatePrincipalModel : BasePrincipalModel
    {
        public List<CredentialModel> Credentials { get; set; }

        public int CustomerId { get; set; }
    }
}
