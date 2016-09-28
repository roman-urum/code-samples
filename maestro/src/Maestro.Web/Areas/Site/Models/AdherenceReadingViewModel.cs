using Maestro.Domain.Dtos.PatientsService.Enums;
using Maestro.Web.Areas.Site.Models.Patients.Charts;
using Maestro.Domain.Dtos.PatientsService.Calendar;

namespace Maestro.Web.Areas.Site.Models
{
    /// <summary>
    /// AdherenceReadingViewModel.
    /// </summary>
    /// <seealso cref="Maestro.Web.Areas.Site.Models.Patients.Charts.BaseReadingViewModel" />
    public class AdherenceReadingViewModel : BaseReadingViewModel
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public AdherenceStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the calendar event.
        /// </summary>
        /// <value>
        /// The calendar event.
        /// </value>
        public CalendarItemResponseDto CalendarEvent { get; set; }
    }
}