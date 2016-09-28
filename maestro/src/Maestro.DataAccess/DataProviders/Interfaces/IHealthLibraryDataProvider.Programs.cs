using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos.HealthLibraryService.Programs;
using Maestro.Domain.Dtos.PatientsService.Calendar;

namespace Maestro.DataAccess.Api.DataProviders.Interfaces
{
    /// <summary>
    /// IHealthLibraryDataProvider.Programs
    /// </summary>
    public partial interface IHealthLibraryDataProvider
    {
        /// <summary>
        /// Searches the programs.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchProgramsDto">The search programs dto.</param>
        /// <returns></returns>
        Task<IList<ProgramResponseDto>> SearchPrograms(string token, int customerId, SearchProgramsRequestDto searchProgramsDto);

        /// <summary>
        /// Gets the program.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programId">The program identifier.</param>
        /// <returns></returns>
        Task<ProgramResponseDto> GetProgram(string token, int customerId, Guid programId);

        /// <summary>
        /// Generates program schedule for calendar.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="programId"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<CreateCalendarProgramRequestDto> GetProgramSchedule(int customerId, Guid programId,
            ProgramScheduleRequestDto searchCriteria, string token);

        /// <summary>
        /// Creates the program.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programDto">The program dto.</param>
        /// <returns></returns>
        Task<CreateProgramResultDto> CreateProgram(string token, int customerId, ProgramRequestDto programDto);

        /// <summary>
        /// Creates the program.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programId">Id of existed program.</param>
        /// <param name="programDto">The program dto.</param>
        /// <returns></returns>
        Task UpdateProgram(string token, int customerId, Guid programId, ProgramRequestDto programDto);
    }
}