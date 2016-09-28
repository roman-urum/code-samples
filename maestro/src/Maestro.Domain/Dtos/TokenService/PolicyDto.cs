namespace Maestro.Domain.Dtos.TokenService
{
    /// <summary>
    /// PloicyDto.
    /// </summary>
    public class PolicyDto
    {
        /// <summary>
        /// Gets or sets the effect.
        /// </summary>
        /// <value>
        /// The effect.
        /// </value>
        public string Effect { get; set; }
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; set; }
        /// <summary>
        /// Gets or sets the vitals.
        /// </summary>
        /// <value>
        /// The vitals.
        /// </value>
        public string Vitals { get; set; }
        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>
        /// The controller.
        /// </value>
        public string Controller { get; set; }
        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        /// <value>
        /// The customer.
        /// </value>
        public int Customer { get; set; }
    }
}
