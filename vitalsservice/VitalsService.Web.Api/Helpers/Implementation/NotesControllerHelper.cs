using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.PatientNotes;

namespace VitalsService.Web.Api.Helpers.Implementation
{
    /// <summary>
    /// NotesControllerHelper.
    /// </summary>
    public class NotesControllerHelper : INotesControllerHelper
    {
        private readonly IPatientNotesService patientNotesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesControllerHelper"/> class.
        /// </summary>
        /// <param name="patientNotesService">The patient notes service.</param>
        public NotesControllerHelper(IPatientNotesService patientNotesService)
        {
            this.patientNotesService = patientNotesService;
        }

        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId"></param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<BaseNoteDto, NoteStatus>> CreateNote(
            int customerId,
            Guid patientId,
            NoteRequestDto request
        )
        {
            var patientNote = Mapper.Map<NoteRequestDto, Note>(request);
            patientNote.CustomerId = customerId;
            patientNote.PatientId = patientId;

            var createNoteResult = await patientNotesService.CreateNote(patientNote);

            var noteDto = createNoteResult.Status == NoteStatus.Success ? Mapper.Map<NoteDetailedResponseDto>(createNoteResult.Content) : null;

            return new OperationResultDto<BaseNoteDto, NoteStatus>()
            {
                Content = noteDto,
                Status = createNoteResult.Status
            };
        }

        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<NoteBriefResponseDto, NoteStatus>> GetNote(int customerId, Guid patientId, Guid noteId)
        {
            var result = await patientNotesService.GetNote(customerId, patientId, noteId);

            if (result == null)
            {
                return await Task.FromResult(
                    new OperationResultDto<NoteBriefResponseDto, NoteStatus>()
                    {
                        Status = NoteStatus.NotFound
                    }
                );
            }

            return await Task.FromResult(
                new OperationResultDto<NoteBriefResponseDto, NoteStatus>()
                {
                    Status = NoteStatus.Success,
                    Content = Mapper.Map<Note, NoteBriefResponseDto>(result)
                }
            );
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResultDto<BaseNoteDto>> GetNotes(int customerId, Guid patientId, NotesSearchDto request)
        {
            var getNotesResult = await patientNotesService.GetNotes(customerId, patientId, request);

            if (request.IsBrief)
            {
                var result = Mapper.Map<PagedResult<Note>, PagedResultDto<NoteBriefResponseDto>>(getNotesResult);

                return new PagedResultDto<BaseNoteDto>()
                {
                    Total = result.Total,
                    Results = result.Results.Cast<BaseNoteDto>().ToList()
                };
            }
            else
            {
                var result = Mapper.Map<PagedResult<Note>, PagedResultDto<NoteDetailedResponseDto>>(getNotesResult);
                return new PagedResultDto<BaseNoteDto>()
                {
                    Total = result.Total,
                    Results = result.Results.Cast<BaseNoteDto>().ToList()
                };
            }
        }

        /// <summary>
        /// Gets the patient note notables.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<IList<string>> GetPatientNoteNotables(int customerId, Guid patientId)
        {
            var result = await patientNotesService.GetPatientNoteNotables(customerId, patientId);

            return Mapper.Map<IList<NoteNotable>, IList<string>>(result)
                .Distinct()
                .ToList();
        }
    }
}