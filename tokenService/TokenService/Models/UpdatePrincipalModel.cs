namespace CareInnovations.HealthHarmony.Maestro.TokenService.Models
{
    /// <summary>
    /// Model for request to update existing principal.
    /// </summary>
    public class UpdatePrincipalModel : BasePrincipalModel
    {
        /// <summary>
        /// New credential.
        /// </summary>
        public CredentialUpdateModel Credential { get; set; }
    }
}