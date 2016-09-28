using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos.HealthLibraryService.Programs;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// HealthLibraryService.Programs
    /// </summary>
    public partial class HealthLibraryService
    {
        /// <summary>
        /// Searches the programs.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchProgramsDto">The search programs dto.</param>
        /// <returns></returns>
        public async Task<IList<ProgramResponseDto>> SearchPrograms(string token, int customerId, SearchProgramsRequestDto searchProgramsDto)
        {
            return await healthLibraryDataProvider.SearchPrograms(token, customerId, searchProgramsDto);
        }

        /// <summary>
        /// Gets the program.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programId">The program identifier.</param>
        /// <returns></returns>
        public async Task<ProgramResponseDto> GetProgram(string token, int customerId, Guid programId)
        {
            return await healthLibraryDataProvider.GetProgram(token, customerId, programId);
        }

        public async Task<CreateProgramResultDto> CreateProgram(string token, int customerId, ProgramRequestDto programDto)
        {
            return await healthLibraryDataProvider.CreateProgram(token, customerId, programDto);
        }

        /// <summary>
        /// Updates the program.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programId">Id of program to update.</param>
        /// <param name="programDto">The program dto.</param>
        /// <returns></returns>
        public async Task UpdateProgram(string token, int customerId, Guid programId,
            ProgramRequestDto programDto)
        {
            await healthLibraryDataProvider.UpdateProgram(token, customerId, programId, programDto);
        }
    }
}