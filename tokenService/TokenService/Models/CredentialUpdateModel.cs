namespace CareInnovations.HealthHarmony.Maestro.TokenService.Models
{
    /// <summary>
    /// Model to change principal credential.
    /// </summary>
    public class CredentialUpdateModel : CredentialModel
    {
        /// <summary>
        /// Current value of credential to verify that it is valid.
        /// </summary>
        public string CurrentValue { get; set; }
    }
}