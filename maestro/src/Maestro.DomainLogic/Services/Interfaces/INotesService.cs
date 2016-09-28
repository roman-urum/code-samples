using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos;
using System.Threading.Tasks;
using Maestro.Domain.Dtos.VitalsService.PatientNotes;

namespace Maestro.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// INotesService.
    /// </summary>
    public interface INotesService
    {
        /// <summary>
        /// Get notes.
        /// </summary>
        /// <param name="searchRequest">The search parameters</param>
        /// <param name="token">The authorization token</param>
        /// <returns>The list of notes</returns>
        Task<PagedResult<BaseNoteResponseDto>> GetNotes(SearchNotesDto searchRequest, string token);

        /// <summary>
        /// Creates note.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="createNote">The note model.</param>
        /// <param name="token">The security token.</param>
        /// <returns></returns>
        Task<NoteDetailedResponseDto> CreateNote(int customerId, Guid patientId, BaseNoteDto createNote, string token);

        /// <summary>
        /// Get notables.
        /// </summary>
        /// <param name="customerId">The customer identifier</param>
        /// <param name="patientId">The patient identifier</param>
        /// <param name="token">The authorization token</param>
        /// <returns>The list of notables</returns>
        Task<List<string>> GetNotables(int customerId, Guid patientId, string token);

        /// <summary>
        /// Gets the list of suggested notables
        /// </summary>
        /// <param name="getNotablesRequest">The search request.</param>
        /// <param name="customerId">The customer identifier</param>
        /// <param name="token">The authorization token</param>
        /// <returns></returns>
        Task<PagedResult<SuggestedNotableDto>> GetSuggestedNotables(BaseSearchDto getNotablesRequest, int customerId, string token);
    }
}