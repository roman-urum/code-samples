using System;

namespace Maestro.Domain.Dtos.TokenService.RequestsResponses
{
    public class CredentialModel
    {
        public string Type { get; set; }

        public string Value { get; set; }

        public bool Disabled { get; set; }

        public DateTime? ExpiresUtc { get; set; }
    }
}
