using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using VitalsService.DataAccess.EF;
using VitalsService.DataAccess.EF.Repositories;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Helpers;

namespace VitalsService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// PatientNotesService.
    /// </summary>
    public class PatientNotesService : IPatientNotesService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<SuggestedNotable> suggestedNotableRepository;
        private readonly IRepository<Vital> vitalRepository;
        private readonly IRepository<Measurement> measurementRepository;
        private readonly IRepository<HealthSessionElement> healthSessionElementRepository;
        private readonly IRepository<Note> noteRepository;
        private readonly IRepository<NoteNotable> noteNotableRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientNotesService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public PatientNotesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.suggestedNotableRepository = unitOfWork.CreateRepository<SuggestedNotable>();
            this.vitalRepository = unitOfWork.CreateRepository<Vital>();
            this.healthSessionElementRepository = unitOfWork.CreateRepository<HealthSessionElement>();
            this.noteRepository = unitOfWork.CreateRepository<Note>();
            this.noteNotableRepository = unitOfWork.CreateRepository<NoteNotable>();
            this.measurementRepository = unitOfWork.CreateRepository<Measurement>();
        }

        /// <summary>
        /// Creates the suggested notable.
        /// </summary>
        /// <param name="suggestedNotable">The suggested notable.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Guid, SuggestedNotableStatus>> CreateSuggestedNotable(SuggestedNotable suggestedNotable)
        {
            var existingSuggestedNotable = await suggestedNotableRepository
               .FirstOrDefaultAsync(
                   e => e.CustomerId == suggestedNotable.CustomerId && 
                    e.Name.ToLower() == suggestedNotable.Name.ToLower()
               );

            if (existingSuggestedNotable != null)
            {
                return new OperationResultDto<Guid, SuggestedNotableStatus>()
                {
                    Status = SuggestedNotableStatus.NameConflict
                };
            }

            suggestedNotableRepository.Insert(suggestedNotable);
            await unitOfWork.SaveAsync();

            return await Task.FromResult(
                new OperationResultDto<Guid, SuggestedNotableStatus>()
                {
                    Status = SuggestedNotableStatus.Success,
                    Content = suggestedNotable.Id
                }
            );
        }

        /// <summary>
        /// Updates the suggested notable.
        /// </summary>
        /// <param name="suggestedNotable">The suggested notable.</param>
        /// <returns></returns>
        public async Task<SuggestedNotableStatus> UpdateSuggestedNotable(SuggestedNotable suggestedNotable)
        {
            var existingSuggestedNotable =
                await GetSuggestedNotable(suggestedNotable.CustomerId, suggestedNotable.Id);

            if (existingSuggestedNotable == null)
            {
                return await Task.FromResult(SuggestedNotableStatus.NotFound);
            }

            var suggestedNotablesWithSimilarName = await suggestedNotableRepository
                .FindAsync(
                    e => e.CustomerId == suggestedNotable.CustomerId &&
                    e.Name.ToLower() == suggestedNotable.Name.ToLower() &&
                    e.Id != suggestedNotable.Id
                );

            if (suggestedNotablesWithSimilarName.Any())
            {
                await Task.FromResult(SuggestedNotableStatus.NameConflict);
            }

            Mapper.Map(suggestedNotable, existingSuggestedNotable, typeof(SuggestedNotable), typeof(SuggestedNotable));

            suggestedNotableRepository.Update(existingSuggestedNotable);
            await unitOfWork.SaveAsync();

            return SuggestedNotableStatus.Success;
        }

        /// <summary>
        /// Deletes the suggested notable.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="suggestedNotableId">The suggested notable identifier.</param>
        /// <returns></returns>
        public async Task<SuggestedNotableStatus> DeleteSuggestedNotable(int customerId, Guid suggestedNotableId)
        {
            var existingSuggestedNotable =
                await GetSuggestedNotable(customerId, suggestedNotableId);

            if (existingSuggestedNotable != null)
            {
                suggestedNotableRepository.Delete(existingSuggestedNotable);

                await unitOfWork.SaveAsync();

                return await Task.FromResult(SuggestedNotableStatus.Success);
            }

            return await Task.FromResult(SuggestedNotableStatus.NotFound);
        }

        /// <summary>
        /// Gets the suggested notable.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="suggestedNotableId">The suggested notable identifier.</param>
        /// <returns></returns>
        public async Task<SuggestedNotable> GetSuggestedNotable(int customerId, Guid suggestedNotableId)
        {
            return (await suggestedNotableRepository
               .FindAsync(
                   e => e.CustomerId == customerId &&
                   e.Id == suggestedNotableId
               )).SingleOrDefault();
        }

        /// <summary>
        /// Gets the suggested notables.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResult<SuggestedNotable>> GetSuggestedNotables(int customerId, BaseSearchDto request)
        {
            Expression<Func<SuggestedNotable, bool>> expression = n => n.CustomerId == customerId;

            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.Q))
                {
                    var terms = request.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(n => n.Name.Contains(term));
                    }
                }
            }

            return (await suggestedNotableRepository
                .FindPagedAsync(
                    expression,
                    o => o.OrderBy(e => e.UpdatedUtc),
                    null,
                    request != null ? request.Skip : (int?)null,
                    request != null ? request.Take : (int?)null
                ));
        }

        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Note, NoteStatus>> CreateNote(Note note)
        {
            NoteStatus checkResult = 0;

            if (note.MeasurementId.HasValue)
            {
                var existingMeasurement = (await measurementRepository.FindAsync( e => e.Id == note.MeasurementId.Value && e.CustomerId == note.CustomerId)).SingleOrDefault();

                if (existingMeasurement == null)
                {
                    checkResult = NoteStatus.MeasurementConflict;
                }
                else
                {
                    note.Measurement = existingMeasurement;
                }
            }

            if (note.HealthSessionElementId.HasValue)
            {
                var existingHealthSessionElement = (await healthSessionElementRepository
                   .FindAsync(
                       e => e.Id == note.HealthSessionElementId.Value &&
                           e.HealthSession.CustomerId == note.CustomerId,
                       null,
                       new List<Expression<Func<HealthSessionElement, object>>>
                       {
                           s => s.HealthSession
                       }
                   )).SingleOrDefault();

                if (existingHealthSessionElement == null)
                {
                    checkResult = NoteStatus.HealthSessionElementConflict;
                }
                else
                {
                    note.HealthSessionElement = existingHealthSessionElement;
                }
            }

            if (checkResult > 0)
            {
                return await Task.FromResult(
                    new OperationResultDto<Note, NoteStatus>()
                    {
                        Status = checkResult
                    }
                ); 
            }

            noteRepository.Insert(note);
            await unitOfWork.SaveAsync();

            return await Task.FromResult(
                new OperationResultDto<Note, NoteStatus>()
                {
                    Status = NoteStatus.Success,
                    Content = note
                }
            );
        }

        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns></returns>
        public async Task<Note> GetNote(int customerId, Guid patientId, Guid noteId)
        {
            return (await noteRepository
               .FindAsync(
                   e => e.CustomerId == customerId &&
                   e.PatientId == patientId &&
                   e.Id == noteId,
                   null,
                   new List<Expression<Func<Note, object>>>
                   {
                       s => s.Notables
                   }
               )).SingleOrDefault();
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResult<Note>> GetNotes(int customerId, Guid patientId, NotesSearchDto request)
        {
            Expression<Func<Note, bool>> expression = n => n.CustomerId == customerId && n.PatientId == patientId;

            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.Q))
                {
                    var terms = request.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(n => n.Text.Contains(term));
                    }
                }

                if (request.Notables != null && request.Notables.Any())
                {
                    Expression<Func<Note, bool>> subExpression = PredicateBuilder.False<Note>();

                    foreach (var notable in request.Notables)
                    {
                        subExpression = subExpression.Or(n => n.Notables.Any(nt => nt.Name.Contains(notable)));
                    }

                    expression = expression.And(subExpression);
                }
            }

            return (await noteRepository
                .FindPagedAsync(
                    expression,
                    o => o.OrderByDescending(e => e.CreatedUtc),
                    null,
                    request != null ? request.Skip : (int?)null,
                    request != null ? request.Take : (int?)null
                ));
        }

        /// <summary>
        /// Gets the patient note notables.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<IList<NoteNotable>> GetPatientNoteNotables(int customerId, Guid patientId)
        {
            return (await noteNotableRepository
                .FindAsync(
                    n => n.Note.CustomerId == customerId && n.Note.PatientId == patientId,
                    null,
                    new List<Expression<Func<NoteNotable, object>>>
                    {
                        s => s.Note
                    }
                ));
        }
    }
}