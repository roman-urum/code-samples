namespace CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Dtos.Enums
{
    /// <summary>
    /// UpdatePrincipalStatus.
    /// </summary>
    public enum UpdatePrincipalStatus
    {
        Success = 1,

        DuplicateUsername = 2,

        NotFound = 3,

        InvalidCurrentCredentialValue = 4,

        CredentialAlreadyUsed = 5
    }
}