namespace Maestro.Web.Areas.Site.Models.Patients.SearchPatients
{
    /// <summary>
    /// Class represents the patient's program info.
    /// </summary>
    public class PatientProgramViewModel
    {
        /// <summary>
        /// Gets or sets the program name.
        /// </summary>
        /// <value>The program name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of current program day.
        /// </summary>
        /// <value>The number of current program day.</value>
        public int CurrentDay { get; set; }

        /// <summary>
        /// Gets or sets the total amount of program days.
        /// </summary>
        /// <value>The total amount of program days.</value>
        public int TotalDays { get; set; }
    }
}