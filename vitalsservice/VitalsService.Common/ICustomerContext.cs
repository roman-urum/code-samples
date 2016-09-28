namespace VitalsService
{
    /// <summary>
    /// ICustomerContext.
    /// </summary>
    public interface ICustomerContext
    {
        /// <summary>
        /// Gets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        int CustomerId { get; }

        /// <summary>
        /// Returns container name for blob storage
        /// for current customer.
        /// </summary>
        /// <returns></returns>
        string GetMediaContainerName();
    }
}