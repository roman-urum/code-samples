namespace Maestro.Domain.Constants
{
    /// <summary>
    /// Container for contants with values of error keys from different services.
    /// </summary>
    public static class ServicesErrors
    {
        /// <summary>
        /// Container for contants with values of error keys from TokenService.
        /// </summary>
        public static class TokenService
        {
            public const string InvalidCredentialValue = "5005";

            public const string CredentialAlreadyUsed = "5006";
        }
    }
}
