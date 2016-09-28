namespace CareInnovations.HealthHarmony.Maestro.TokenService.Domain
{
    /// <summary>
    /// Indicates that <see cref="Entity"/> supports soft delete only.
    /// </summary>
    public interface ISoftDelitable
    {
        /// <summary>
        /// Indicating whether entity deleted or not.
        /// </summary>
        bool IsDeleted { get; set; }
    }
}