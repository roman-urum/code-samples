using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HealthLibrary.Common.Extensions;
using HealthLibrary.Common.Helpers;
using HealthLibrary.DataAccess;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Program;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.DomainLogic.Services.Results;

namespace HealthLibrary.DomainLogic.Services.Implementations
{
    public class ProgramService : IProgramService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Program> programRepository;
        private readonly IRepository<ProgramElement> programElementRepository;
        private readonly IRepository<ProgramDayElement> programDayElementRepository;
        private readonly IRepository<Recurrence> recurrenceRepository;

        public ProgramService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.programRepository = this.unitOfWork.CreateGenericRepository<Program>();
            this.programElementRepository = this.unitOfWork.CreateGenericRepository<ProgramElement>();
            this.programDayElementRepository = this.unitOfWork.CreateGenericRepository<ProgramDayElement>();
            this.recurrenceRepository = this.unitOfWork.CreateGenericRepository<Recurrence>();
        }

        public async Task<ServiceActionResult<Program>> Create(Program program)
        {
            this.programRepository.Insert(program);

            await this.unitOfWork.SaveAsync();

            return new ServiceActionResult<Program>(ServiceActionResultStatus.Succeed) { Content = program };
        }

        public async Task<Program> Get(int customerId, Guid programId)
        {
            return (await programRepository.FindAsync(
                p => !p.IsDeleted && p.Id == programId && p.CustomerId == customerId,
                null,
                new List<Expression<Func<Program, object>>>
                {
                    e => e.Tags,
                    e => e.ProgramElements,
                    e => e.ProgramDayElements,
                    e => e.Recurrences
                }
            )).FirstOrDefault();
        }

        public async Task<CreateUpdateProgramStatus> Update(int customerId, Guid programId, Program newProgram)
        {
            var existingProgram = await Get(customerId, programId);

            if (existingProgram == null) return CreateUpdateProgramStatus.NotFound;

            existingProgram.Name = newProgram.Name;
            existingProgram.Tags.Clear();
            existingProgram.Tags.AddRange(newProgram.Tags);

            var programElements = existingProgram.ProgramElements.ToList();
            programElements.ForEach(pe => pe.ProgramDayElements.RemoveRange(pe.ProgramDayElements.ToList()));
            
            var programDayElements = existingProgram.ProgramDayElements.ToList();

            this.programDayElementRepository.DeleteRange(programDayElements);
            this.programElementRepository.DeleteRange(programElements);
            this.recurrenceRepository.DeleteRange(existingProgram.Recurrences.ToList());

            existingProgram.ProgramElements.AddRange(newProgram.ProgramElements);
            existingProgram.ProgramDayElements.AddRange(newProgram.ProgramDayElements);                                    
            existingProgram.Recurrences.AddRange(newProgram.Recurrences);
            
            this.programRepository.Update(existingProgram);

            await this.unitOfWork.SaveAsync();

            return CreateUpdateProgramStatus.Success;
        }

        public async Task<DeleteProgramStatus> Delete(int customerId, Guid programId)
        {
            var existingProgram = await Get(customerId, programId);

            if (existingProgram == null) return DeleteProgramStatus.NotFound;

            this.programRepository.Delete(existingProgram);

            await this.unitOfWork.SaveAsync();

            return DeleteProgramStatus.Success;
        }

        public async Task<PagedResult<Program>> FindPrograms(int customerId, SearchProgramDto request = null)
        {
            Expression<Func<Program, bool>> expression = p => !p.IsDeleted && p.CustomerId == customerId;

            if (request != null)
            {
                if (request.CreatedAfter.HasValue)
                {
                    expression = expression.And(p => p.CreatedUtc >= request.CreatedAfter.Value);
                }

                if (request.UpdatedAfter.HasValue)
                {
                    expression = expression.And(p => p.UpdatedUtc >= request.UpdatedAfter.Value);
                }

                if (request.CreatedBefore.HasValue)
                {
                    expression = expression.And(p => p.CreatedUtc < request.CreatedBefore.Value);
                }

                if (request.UpdatedBefore.HasValue)
                {
                    expression = expression.And(p => p.UpdatedUtc < request.UpdatedBefore.Value);
                }

                if (request.Tags != null && request.Tags.Any())
                {
                    Expression<Func<Program, bool>> tagsExpression = PredicateBuilder.False<Program>(); ;

                    foreach (var tag in request.Tags)
                    {
                        tagsExpression = tagsExpression.Or(se => se.Tags.Any(t => t.Name.ToLower() == tag.ToLower()));
                    }

                    expression = expression.And(tagsExpression);
                }

                if (!string.IsNullOrEmpty(request.Q))
                {
                    var terms = request.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(p => p.Name.Contains(term));
                    }
                }
            }

            return await programRepository
                .FindPagedAsync(
                    expression,
                    o => o.OrderBy(e => e.Name),
                    new List<Expression<Func<Program, object>>>
                    {
                        e => e.Tags,
                        e => e.ProgramElements,
                        e => e.ProgramDayElements,
                        e => e.Recurrences
                    },
                    request != null ? request.Skip : (int?)null,
                    request != null ? request.Take : (int?)null
                );
        }
    }
}