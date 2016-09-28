using System;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Models
{
    /// <summary>
    /// Model for success response of validation request.
    /// </summary>
    public class VerifyAccessResponseModel
    {
        public string Id { get; set; }

        public bool Allowed { get; set; }

        public double TTL { get; set; }

        /// <summary>
        /// Fullname of token owner.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Id of principal which was used to create token.
        /// </summary>
        public Guid PrincipalId { get; set; }
    }
}