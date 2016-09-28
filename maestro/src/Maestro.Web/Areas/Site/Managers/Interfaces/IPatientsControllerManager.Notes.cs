using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Web.Areas.Site.Models.Patients.Notes;

namespace Maestro.Web.Areas.Site.Managers.Interfaces
{
    /// <summary>
    /// IPatientsControllerManager.Notes
    /// </summary>
    public partial interface IPatientsControllerManager
    {
        /// <summary>
        /// Searches the notes.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns>The list of notes</returns>
        Task<PagedResult<BaseNoteResponseViewModel>> GetNotes(SearchNotesViewModel searchModel);

        /// <summary>
        /// Gets the notables.
        /// </summary>
        /// <param name="patientId">The patient idetifier.</param>
        /// <returns>The list of notables</returns>
        Task<List<string>> GetNotables(Guid patientId);


        /// <summary>
        /// Gets the list of suggested notables.
        /// </summary>
        /// <returns></returns>
        Task<IList<string>> GetSuggestedNotables();
    }
}