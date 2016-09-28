using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients.Calendar
{
    /// <summary>
    /// Model for retrieving adherences together with coresponding calendar programs
    /// </summary>
    public class GetAdherencesAndProgramsViewModel
    {
        /// <summary>
        /// Gets or sets the list of adherences.
        /// </summary>
        /// <value>
        /// The list of adherences.
        /// </value>
        public IList<AdherenceViewModel> Adherences { get; set; }

        /// <summary>
        /// Gets ot sets the list of calendar programs.
        /// </summary>
        /// <value>
        /// The list of calendar programs.
        /// </value>
        public IList<CalendarProgramViewModel> Programs { get; set; }
    }
}