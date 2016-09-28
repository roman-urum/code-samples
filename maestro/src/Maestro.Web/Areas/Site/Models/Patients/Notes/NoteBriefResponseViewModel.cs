using System;

namespace Maestro.Web.Areas.Site.Models.Patients.Notes
{
    /// <summary>
    /// The respone model for notes 
    /// </summary>
    public class NoteBriefResponseViewModel: BaseNoteResponseViewModel
    {
        /// <summary>
        /// Gets or sets the vital reading.
        /// </summary>
        /// <value>
        /// The vital reading.
        /// </value>
        public Guid? MeasurementId { get; set; }

        /// <summary>
        /// Gets or sets the health session element reading.
        /// </summary>
        /// <value>
        /// The health session element reading.
        /// </value>
        public Guid? HealthSessionElementId { get; set; }
    }
}