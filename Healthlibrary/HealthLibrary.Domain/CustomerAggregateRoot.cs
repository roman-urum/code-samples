namespace HealthLibrary.Domain
{
    /// <summary>
    /// Common class for entities which can be generic or
    /// assigned to specific customer.
    /// </summary>
    public abstract class CustomerAggregateRoot : AggregateRoot
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }
    }
}