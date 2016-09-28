using System;

namespace Maestro.Domain.Dtos.TokenService.RequestsResponses
{
    public class TokenResponseModel
    {
        /// <summary>
        /// Utc date and time when current credential is expires.
        /// </summary>
        public DateTime? ExpirationUtc { get; set; }

        public string Id { get; set; }

        public bool Success { get; set; }

        public int TTL { get; set; }
    }
}