namespace CareInnovations.HealthHarmony.Maestro.TokenService.Common.Helpers
{
    /// <summary>
    /// HashGenerator.
    /// </summary>
    public static class HashGenerator
    {
        /// <summary>
        /// Generates hash for provided value if it possible.
        /// If not - returns original value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetHash(string value)
        {
            if (!ModularCrypt.ValidMcf(value))
            {
                return ModularCrypt.Derive(value);
            }

            return value;
        }
    }
}
