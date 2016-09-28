namespace HealthLibrary.Domain.Entities
{
    /// <summary>
    /// Includes fields for internal and external ids.
    /// </summary>
    public interface IBaseAnalyticsEntity
    {
        /// <summary>
        /// Gets or sets the internal question id.
        /// </summary>
        string InternalId { get; set; }

        /// <summary>
        /// Gets or sets the external questino id.
        /// </summary>
        string ExternalId { get; set; }
    }
}
