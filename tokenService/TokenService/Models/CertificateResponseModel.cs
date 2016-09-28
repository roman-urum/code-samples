namespace CareInnovations.HealthHarmony.Maestro.TokenService.Models
{
    /// <summary>
    /// Model for response data of request to verify access rights.
    /// </summary>
    public class CertificateResponseModel
    {
        /// <summary>
        /// Identify if certificate has access to specified patient.
        /// </summary>
        public bool IsAllowed { get; set; }
    }
}