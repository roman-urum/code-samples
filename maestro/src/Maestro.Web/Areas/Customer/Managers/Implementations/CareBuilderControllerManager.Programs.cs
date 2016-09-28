using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Maestro.Domain.Dtos.HealthLibraryService.Programs;

namespace Maestro.Web.Areas.Customer.Managers.Implementations
{
    /// <summary>
    /// CareBuilderControllerManager.
    /// </summary>
    public partial class CareBuilderControllerManager
    {
        /// <summary>
        /// Gets the program.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <returns></returns>
        public async Task<ProgramResponseDto> GetProgram(Guid programId)
        {
            var token = authDataStorage.GetToken();

            return await healthLibraryService.GetProgram(token, CustomerContext.Current.Customer.Id, programId);
        }

        /// <summary>
        /// Searches the programs.
        /// </summary>
        /// <param name="searchProgramsDto">The search programs dto.</param>
        /// <returns></returns>
        public async Task<IList<ProgramResponseDto>> SearchPrograms(SearchProgramsRequestDto searchProgramsDto)
        {
            var token = authDataStorage.GetToken();

            return await healthLibraryService.SearchPrograms(token, CustomerContext.Current.Customer.Id, searchProgramsDto);
        }

        /// <summary>
        /// Send request to HL to create new program.
        /// </summary>
        /// <param name="programModel"></param>
        /// <returns></returns>
        public async Task<CreateProgramResultDto> CreateProgram(ProgramRequestDto programModel)
        {
            var programDto = Mapper.Map<ProgramRequestDto>(programModel);

            var token = this.authDataStorage.GetToken();

            var createProgramResult = await healthLibraryService.CreateProgram(token, this.customerContext.Customer.Id, programDto);

            return createProgramResult;
        }

        /// <summary>
        /// Send request to HL to update program.
        /// </summary>
        /// <param name="id">Id of program to update.</param>
        /// <param name="programModel"></param>
        /// <returns></returns>
        public async Task UpdateProgram(Guid id, ProgramRequestDto programModel)
        {
            var token = this.authDataStorage.GetToken();

            await healthLibraryService.UpdateProgram(token, this.customerContext.Customer.Id, id, programModel);
        }
    }
}