using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;

namespace VitalsService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IPatientNotesService.
    /// </summary>
    public interface IPatientNotesService
    {
        /// <summary>
        /// Creates the suggested notable.
        /// </summary>
        /// <param name="suggestedNotable">The suggested notable.</param>
        /// <returns></returns>
        Task<OperationResultDto<Guid, SuggestedNotableStatus>> CreateSuggestedNotable(SuggestedNotable suggestedNotable);

        /// <summary>
        /// Updates the suggested notable.
        /// </summary>
        /// <param name="suggestedNotable">The suggested notable.</param>
        /// <returns></returns>
        Task<SuggestedNotableStatus> UpdateSuggestedNotable(SuggestedNotable suggestedNotable);

        /// <summary>
        /// Deletes the suggested notable.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="suggestedNotableId">The suggested notable identifier.</param>
        /// <returns></returns>
        Task<SuggestedNotableStatus> DeleteSuggestedNotable(int customerId, Guid suggestedNotableId);

        /// <summary>
        /// Gets the suggested notable.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="suggestedNotableId">The suggested notable identifier.</param>
        /// <returns></returns>
        Task<SuggestedNotable> GetSuggestedNotable(int customerId, Guid suggestedNotableId);

        /// <summary>
        /// Gets the suggested notables.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResult<SuggestedNotable>> GetSuggestedNotables(int customerId, BaseSearchDto request);

        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns></returns>
        Task<OperationResultDto<Note, NoteStatus>> CreateNote(Note note);

        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns></returns>
        Task<Note> GetNote(int customerId, Guid patientId, Guid noteId);

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResult<Note>> GetNotes(int customerId, Guid patientId, NotesSearchDto request);

        /// <summary>
        /// Gets the patient note notables.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        Task<IList<NoteNotable>> GetPatientNoteNotables(int customerId, Guid patientId);
    }
}