using System;
using Maestro.Domain.Dtos.PatientsService.Enums;

namespace Maestro.Web.Areas.Site.Models.Patients.Calendar
{
    /// <summary>
    /// Base class for view models with calendar changes.
    /// </summary>
    public abstract class CalendarChangeViewModel
    {
        /// <summary>
        /// Name of user who made changes.
        /// </summary>
        public string ChangedBy { get; set; }

        /// <summary>
        /// Date and time when changes has been applied.
        /// </summary>
        public DateTime ChangedUtc { get; set; }

        /// <summary>
        /// Name of element which was changed.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of calendar element.
        /// </summary>
        public CalendarElementType ElementType { get; set; }
    }
}