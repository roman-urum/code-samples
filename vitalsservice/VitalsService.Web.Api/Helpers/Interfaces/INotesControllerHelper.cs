using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.PatientNotes;

namespace VitalsService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// INotesControllerHelper.
    /// </summary>
    public interface INotesControllerHelper
    {
        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<OperationResultDto<BaseNoteDto, NoteStatus>> CreateNote(
            int customerId, 
            Guid patientId,
            NoteRequestDto request
        );

        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns></returns>
        Task<OperationResultDto<NoteBriefResponseDto, NoteStatus>> GetNote(
            int customerId,
            Guid patientId, 
            Guid noteId
        );

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResultDto<BaseNoteDto>> GetNotes(int customerId, Guid patientId, NotesSearchDto request);

        /// <summary>
        /// Gets the patient note notables.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        Task<IList<string>> GetPatientNoteNotables(int customerId, Guid patientId);
    }
}