using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService.PatientNotes;
using Maestro.DomainLogic.Services.Interfaces;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// NotesService.
    /// </summary>
    public class NotesService : INotesService
    {
        private readonly INotesDataProvider notesDataProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesService"/> class.
        /// </summary>
        /// <param name="notesDataProvider">The notesDataProvider</param>
        public NotesService(INotesDataProvider notesDataProvider)
        {
            this.notesDataProvider = notesDataProvider;
        }

        /// <summary>
        /// Creates note.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="createNote">The note model.</param>
        /// <param name="token">The security token.</param>
        /// <returns></returns>
        public async Task<NoteDetailedResponseDto> CreateNote(int customerId, Guid patientId, BaseNoteDto createNote, string token)
        {
            return await notesDataProvider.CreateNote(customerId, patientId, createNote, token);
        }

        /// <summary>
        /// Get notables.
        /// </summary>
        /// <param name="customerId">The customer identifier</param>
        /// <param name="patientId">The patient identifier</param>
        /// <param name="token">The authorization token</param>
        /// <returns>The list of notables</returns>
        public async Task<List<string>> GetNotables(int customerId, Guid patientId, string token)
        {
            return await notesDataProvider.GetNotables(customerId, patientId, token);
        }

        /// <summary>
        /// Get notes.
        /// </summary>
        /// <param name="searchRequest">The search parameters</param>
        /// <param name="token">The authorization token</param>
        /// <returns>The list of notes</returns>
        public async Task<PagedResult<BaseNoteResponseDto>> GetNotes(SearchNotesDto searchRequest, string token)
        {
            return await notesDataProvider.GetNotes(searchRequest, token);
        }

        /// <summary>
        /// Gets the list of suggested notables
        /// </summary>
        /// <param name="getNotablesRequest">The search request.</param>
        /// <param name="customerId">The customer identifier</param>
        /// <param name="token">The authorization token</param>
        /// <returns></returns>
        public async Task<PagedResult<SuggestedNotableDto>> GetSuggestedNotables(BaseSearchDto getNotablesRequest, int customerId, string token)
        {
            return await notesDataProvider.GetSuggestedNotables(getNotablesRequest, customerId, token);
        }
    }
}
