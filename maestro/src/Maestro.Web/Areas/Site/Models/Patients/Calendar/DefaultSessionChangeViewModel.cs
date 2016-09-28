using Maestro.Domain.Dtos.PatientsService.Enums;

namespace Maestro.Web.Areas.Site.Models.Patients.Calendar
{
    public class DefaultSessionChangeViewModel : CalendarChangeViewModel
    {
        /// <summary>
        /// Action performed with health session.
        /// </summary>
        public DefaultSessionAction Action { get; set; }
    }
}