namespace Maestro.Web.Areas.Customer.Models.CareBuilder
{
    /// <summary>
    /// Model for search program result
    /// </summary>
    public class SearchProgramResponseViewModel: SearchEntryResponseViewModel
    {
        /// <summary>
        /// Gets ot set the duration in days
        /// </summary>
        /// <value>
        /// The duration in days
        /// </value>
        public int DurationDays { get; set; }
    }
}