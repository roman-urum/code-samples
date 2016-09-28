using System;
using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Programs;

namespace HealthLibrary.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// IProgramsControllerHelper.
    /// </summary>
    public interface IProgramsControllerHelper
    {
        /// <summary>
        /// Creates the program.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programCreateDto">The program create dto.</param>
        /// <returns></returns>
        Task<OperationResultDto<ProgramBriefResponseDto, CreateUpdateProgramStatus>> CreateProgram(
            int customerId, 
            ProgramRequestDto programCreateDto
        );

        /// <summary>
        /// Updates the program.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programId">The identifier.</param>
        /// <param name="updateProgramDto">The update program dto.</param>
        /// <returns></returns>
        Task<CreateUpdateProgramStatus> UpdateProgram(
            int customerId,
            Guid programId, 
            ProgramRequestDto updateProgramDto
        );

        /// <summary>
        /// Deletes the program.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programId">The identifier.</param>
        /// <returns></returns>
        Task<DeleteProgramStatus> DeleteProgram(int customerId, Guid programId);

        /// <summary>
        /// Returns list of programs by specified criteria.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchProgram">The search program.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        Task<PagedResultDto<ProgramBriefResponseDto>> FindPrograms(
            int customerId,
            SearchProgramDto searchProgram,
            bool isBrief
        );

        /// <summary>
        /// Returns program response model for program with specified id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programId">The identifier.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        Task<OperationResultDto<ProgramBriefResponseDto, GetProgramStatus>> GetProgram(
            int customerId,
            Guid programId,
            bool isBrief
        );

        /// <summary>
        /// Generates list of program events in accordance with provided criteria.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programId">The program identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<ProgramScheduleDto> GetProgramSchedule(
            int customerId,
            Guid programId,
            ProgramScheduleRequestDto model
        );
    }
}