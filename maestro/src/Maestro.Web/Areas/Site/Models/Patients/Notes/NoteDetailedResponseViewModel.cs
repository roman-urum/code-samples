using Maestro.Web.Areas.Site.Models.Patients.Charts;

namespace Maestro.Web.Areas.Site.Models.Patients.Notes
{
    /// <summary>
    /// The respone model for notes 
    /// </summary>
    public class NoteDetailedResponseViewModel : BaseNoteResponseViewModel
    {
        /// <summary>
        /// Gets or sets the vital reading.
        /// </summary>
        /// <value>
        /// The vital reading.
        /// </value>
        public MeasurementBriefViewModel MeasurementReading { get; set; }

        /// <summary>
        /// Gets or sets the health session element reading.
        /// </summary>
        /// <value>
        /// The health session element reading.
        /// </value>
        public HealthSessionElementViewModel HealthSessionElementReading { get; set; }
    }
}