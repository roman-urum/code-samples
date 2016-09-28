namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// HealthSessionElementAlert.
    /// </summary>
    public class HealthSessionElementAlert : Alert
    {
        /// <summary>
        /// Gets or sets the health session elements.
        /// </summary>
        /// <value>
        /// The health session elements.
        /// </value>
        public virtual HealthSessionElement HealthSessionElement { get; set; }
    }
}