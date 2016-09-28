using System;
using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Program;
using HealthLibrary.DomainLogic.Services.Results;

namespace HealthLibrary.DomainLogic.Services.Interfaces
{
    public interface IProgramService
    {
        Task<ServiceActionResult<Program>> Create(Program program);

        Task<Program> Get(int customerId, Guid programId);

        Task<CreateUpdateProgramStatus> Update(int customerId, Guid programId, Program newProgram);

        Task<DeleteProgramStatus> Delete(int customerId, Guid programId);

        Task<PagedResult<Program>> FindPrograms(int customerId, SearchProgramDto request = null);
    }
}