using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos.HealthLibraryService.Programs;

namespace Maestro.Web.Areas.Customer.Managers.Interfaces
{
    public partial interface ICareBuilderControllerManager
    {
        /// <summary>
        /// Searches the programs.
        /// </summary>
        /// <param name="searchProgramsDto">The search programs dto.</param>
        /// <returns></returns>
        Task<IList<ProgramResponseDto>> SearchPrograms(SearchProgramsRequestDto searchProgramsDto);

        /// <summary>
        /// Gets the program.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <returns></returns>
        Task<ProgramResponseDto> GetProgram(Guid programId);

        /// <summary>
        /// Send request to HL to create new program.
        /// </summary>
        /// <param name="programModel"></param>
        /// <returns></returns>
        Task<CreateProgramResultDto> CreateProgram(ProgramRequestDto programModel);

        /// <summary>
        /// Send request to HL to update program.
        /// </summary>
        /// <param name="id">Id of program to update.</param>
        /// <param name="programModel"></param>
        /// <returns></returns>
        Task UpdateProgram(Guid id, ProgramRequestDto programModel);
    }
}