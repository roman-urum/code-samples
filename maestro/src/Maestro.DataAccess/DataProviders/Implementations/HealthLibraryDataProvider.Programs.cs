using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.HealthLibraryService.Programs;
using Maestro.Domain.Dtos.PatientsService.Calendar;
using RestSharp;

namespace Maestro.DataAccess.Api.DataProviders.Implementations
{
    /// <summary>
    /// HealthLibraryDataProvider.Programs
    /// </summary>
    public partial class HealthLibraryDataProvider
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
            var url = string.Format("/api/{0}/programs/", customerId);

            var pagedResult = await this.apiClient.SendRequestAsync<PagedResult<ProgramResponseDto>>(url, searchProgramsDto, Method.GET, null, token);

            return pagedResult.Results;
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
            var url = string.Format("/api/{0}/programs/{1}?isBrief=false", customerId, programId);

            return await this.apiClient.SendRequestAsync<ProgramResponseDto>(url, null, Method.GET, null, token);
        }

        /// <summary>
        /// Generates program schedule for calendar.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="programId"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<CreateCalendarProgramRequestDto> GetProgramSchedule(int customerId, Guid programId,
            ProgramScheduleRequestDto searchCriteria, string token)
        {
            var url = string.Format("/api/{0}/programs/{1}/schedule", customerId, programId);

            return await this.apiClient.SendRequestAsync<CreateCalendarProgramRequestDto>(
                url, searchCriteria, Method.GET, null, token);
        }

        public async Task<CreateProgramResultDto> CreateProgram(string token, int customerId, ProgramRequestDto programDto)
        {
            var url = string.Format("/api/{0}/programs", customerId);

            return await this.apiClient.SendRequestAsync<CreateProgramResultDto>(url, programDto, Method.POST, null, token);
        }

        /// <summary>
        /// Creates the program.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programId">Id of existed program.</param>
        /// <param name="programDto">The program dto.</param>
        /// <returns></returns>
        public async Task UpdateProgram(string token, int customerId, Guid programId,
            ProgramRequestDto programDto)
        {
            var url = string.Format("/api/{0}/programs/{1}", customerId, programId);

            await this.apiClient.SendRequestAsync(url, programDto, Method.PUT, null, token);
        }
    }
}