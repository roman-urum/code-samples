namespace Maestro.Web.Areas.Customer.Models.CareBuilder.Tags
{
    /// <summary>
    /// View model for tag data in conditions tags cloud.
    /// </summary>
    public class CloudTagViewModel
    {
        /// <summary>
        /// String value of tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Rate of tag in cloud.
        /// </summary>
        public int Rate { get; set; }
    }
}