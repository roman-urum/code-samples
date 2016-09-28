namespace DeviceService.Common
{
    /// <summary>
    /// ICustomerContextю
    /// </summary>
    public interface ICustomerContext
    {
        /// <summary>
        /// Gets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        int? CustomerId { get; }
    }
}