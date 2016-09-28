using System;

namespace Maestro.Domain.Dtos.TokenService.RequestsResponses
{
    /// <summary>
    /// Model for response with principal details.
    /// </summary>
    public class PrincipalResponseModel : BasePrincipalModel
    {
        public Guid Id { get; set; }
    }
}
