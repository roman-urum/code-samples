using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos.HealthLibraryService.Programs;

namespace Maestro.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IHealthLibraryService.Programs
    /// </summary>
    public partial interface IHealthLibraryService
    {
        /// <summary>
        /// Searches the programs.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchProgramsDto">The search programs dto.</param>
        /// <returns></returns>
        Task<IList<ProgramResponseDto>> SearchPrograms(string token, int customerId,
            SearchProgramsRequestDto searchProgramsDto);

        /// <summary>
        /// Gets the program.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programId">The program identifier.</param>
        /// <returns></returns>
        Task<ProgramResponseDto> GetProgram(string token, int customerId, Guid programId);

        /// <summary>
        /// Creates the program.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programDto">The program dto.</param>
        /// <returns></returns>
        Task<CreateProgramResultDto> CreateProgram(string token, int customerId, ProgramRequestDto programDto);

        /// <summary>
        /// Updates the program.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programId">Id of program to update.</param>
        /// <param name="programDto">The program dto.</param>
        /// <returns></returns>
        Task UpdateProgram(string token, int customerId, Guid programId, ProgramRequestDto programDto);
    }
}