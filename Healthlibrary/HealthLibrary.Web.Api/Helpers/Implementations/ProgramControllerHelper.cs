using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Program;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Programs;
using NodaTime;

namespace HealthLibrary.Web.Api.Helpers.Implementations
{
    /// <summary>
    /// ProgramControllerHelper.
    /// </summary>
    public class ProgramControllerHelper : IProgramsControllerHelper
    {
        private readonly IProgramService programService;
        private readonly ITagsService tagsService;
        private readonly IGlobalSearchCacheHelper globalSearchCacheHelper;
        private readonly ITagsSearchCacheHelper tagsSearchCacheHelper;
        private readonly IProtocolService protocolService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramControllerHelper" /> class.
        /// </summary>
        /// <param name="programService">The program service.</param>
        /// <param name="tagsService">The tags service.</param>
        /// <param name="globalSearchCacheHelper">The global search cache helper.</param>
        /// <param name="tagsSearchCacheHelper">The tags search cache helper.</param>
        /// <param name="protocolService">The protocol service.</param>
        public ProgramControllerHelper(
            IProgramService programService,
            ITagsService tagsService,
            IGlobalSearchCacheHelper globalSearchCacheHelper,
            ITagsSearchCacheHelper tagsSearchCacheHelper,
            IProtocolService protocolService
        )
        {
            this.programService = programService;
            this.tagsService = tagsService;
            this.globalSearchCacheHelper = globalSearchCacheHelper;
            this.tagsSearchCacheHelper = tagsSearchCacheHelper;
            this.protocolService = protocolService;
        }

        /// <summary>
        /// Initializes program entity using request data. Saves entities in database.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programCreateDto">The program create dto.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<ProgramBriefResponseDto, CreateUpdateProgramStatus>> CreateProgram(
            int customerId,
            ProgramRequestDto programCreateDto
        )
        {
            var newProgram = await BuildProgram(customerId, programCreateDto);

            if (newProgram.Status != CreateUpdateProgramStatus.Success)
            {
                return new OperationResultDto<ProgramBriefResponseDto, CreateUpdateProgramStatus>()
                {
                    Status = newProgram.Status
                };
            }

            var createProgramResponse = await programService.Create(newProgram.Content);

            var respoDto = Mapper.Map<ProgramBriefResponseDto>(createProgramResponse.Content);

            await globalSearchCacheHelper.AddOrUpdateEntry(customerId, Mapper.Map<Program, SearchProgramResponseDto>(createProgramResponse.Content));
            await tagsSearchCacheHelper.AddOrUpdateTags(customerId, createProgramResponse.Content.Tags.Select(t => t.Name).ToList());

            var unusedTags = await tagsService.RemoveUnusedTags(customerId);
            await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);

            return new OperationResultDto<ProgramBriefResponseDto, CreateUpdateProgramStatus>()
            {
                Status = newProgram.Status,
                Content = respoDto
            };
        }

        /// <summary>
        /// Generates Program instance using data of ProgramRequestDto.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programDto">The program dto.</param>
        /// <returns></returns>
        private async Task<OperationResultDto<Program, CreateUpdateProgramStatus>> BuildProgram(int customerId, ProgramRequestDto programDto)
        {
            var resultProgram = Mapper.Map<Program>(programDto);

            resultProgram.CustomerId = customerId;
            resultProgram.Tags = await tagsService.BuildTagsList(customerId, programDto.Tags);
            resultProgram.ProgramElements = new List<ProgramElement>();

            foreach (var programElementDto in programDto.ProgramElements)
            {
                var programElement = Mapper.Map<ProgramElement>(programElementDto);

                var protocol = await this.protocolService.GetProtocol(customerId, programElement.ProtocolId);

                if (protocol == null)
                {
                    return new OperationResultDto<Program, CreateUpdateProgramStatus>()
                    {
                        Content = null,
                        Status = CreateUpdateProgramStatus.InvalidProtocolId
                    };
                }

                foreach (var programDayElementDto in programElementDto.ProgramDayElements)
                {
                    var programDayElement = BuildProgramDayElement(programDayElementDto, resultProgram.Recurrences);

                    if (programDayElement == null)
                    {
                        return new OperationResultDto<Program, CreateUpdateProgramStatus>()
                        {
                            Status = CreateUpdateProgramStatus.InvalidRecurrenceReferenceProvided
                        };
                    }

                    programElement.ProgramDayElements.Add(programDayElement);
                    resultProgram.ProgramDayElements.Add(programDayElement);
                }

                resultProgram.ProgramElements.Add(programElement);
            }
            
            if (resultProgram.Recurrences != null)
            {
                resultProgram.Recurrences.Each(r => r.Id = default(Guid));
            }

            return new OperationResultDto<Program, CreateUpdateProgramStatus>()
            {
                Status = CreateUpdateProgramStatus.Success,
                Content = resultProgram
            };
        }

        /// <summary>
        /// Generates ProgramDayElement instance using dto data.
        /// </summary>
        /// <param name="programDayElementDto"></param>
        /// <param name="recurrences"></param>
        /// <returns></returns>
        private ProgramDayElement BuildProgramDayElement(
            ProgramDayElementDto programDayElementDto,
            IEnumerable<Recurrence> recurrences
        )
        {
            var programDayElement = Mapper.Map<ProgramDayElement>(programDayElementDto);

            if (programDayElementDto.RecurrenceId.HasValue)
            {
                var targetRecurrence = recurrences.FirstOrDefault(r => r.Id == programDayElementDto.RecurrenceId);

                if (targetRecurrence == null)
                {
                    return null;
                }

                programDayElement.Recurrence = targetRecurrence;
            }

            return programDayElement;
        }

        /// <summary>
        /// Updates the program.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="programId">The identifier.</param>
        /// <param name="updateProgramDto">The update program dto.</param>
        /// <returns></returns>
        public async Task<CreateUpdateProgramStatus> UpdateProgram(
            int customerId,
            Guid programId,
            ProgramRequestDto updateProgramDto
        )
        {
            var updatedProgram = await BuildProgram(customerId, updateProgramDto);

            if (updatedProgram.Status != CreateUpdateProgramStatus.Success)
            {
                return updatedProgram.Status;
            }

            var result = await programService.Update(customerId, programId, updatedProgram.Content);

            updatedProgram.Content.Id = programId;
            await globalSearchCacheHelper.AddOrUpdateEntry(customerId, Mapper.Map<Program, SearchProgramResponseDto>(updatedProgram.Content));

            await tagsSearchCacheHelper.AddOrUpdateTags(customerId, updatedProgram.Content.Tags.Select(t => t.Name).ToList());

            var unusedTags = await tagsService.RemoveUnusedTags(customerId);
            await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);

            return result;
        }

        /// <summary>
        /// Deletes the program.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programId">The identifier.</param>
        /// <returns></returns>
        public async Task<DeleteProgramStatus> DeleteProgram(int customerId, Guid programId)
        {
            var result = await programService.Delete(customerId, programId);

            if (result == DeleteProgramStatus.Success)
            {
                await globalSearchCacheHelper.RemoveEntry(customerId, programId);

                var unusedTags = await tagsService.RemoveUnusedTags(customerId);
                await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);
            }

            return result;
        }

        /// <summary>
        /// Returns list of programs by specified criteria.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchProgram">The search program.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        public async Task<PagedResultDto<ProgramBriefResponseDto>> FindPrograms(
            int customerId, 
            SearchProgramDto searchProgram, 
            bool isBrief
        )
        {
            var programs = await programService.FindPrograms(customerId, searchProgram);

            if (isBrief)
            {
                return new PagedResultDto<ProgramBriefResponseDto>()
                {
                    Total = programs.Total,
                    Results = Mapper.Map<IList<Program>, IList<ProgramBriefResponseDto>>(programs.Results)
                };
            }

            var mappedPrograms = Mapper.Map<IList<Program>, IList<ProgramResponseDto>>(programs.Results);

            return new PagedResultDto<ProgramBriefResponseDto>()
            {
                Total = programs.Total,
                Results =mappedPrograms.Select(p => (ProgramBriefResponseDto)p).ToList()
            };
        }

        /// <summary>
        /// Returns program response model for program with specified id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programId">The identifier.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        public async Task<OperationResultDto<ProgramBriefResponseDto, GetProgramStatus>> GetProgram(
            int customerId, 
            Guid programId,
            bool isBrief
        )
        {
            var program = await this.programService.Get(customerId, programId);

            if (program == null)
            {
                return new OperationResultDto<ProgramBriefResponseDto, GetProgramStatus>()
                {
                    Status = GetProgramStatus.NotFound
                };
            }

            var content = isBrief
                ? Mapper.Map<ProgramBriefResponseDto>(program)
                : Mapper.Map<ProgramResponseDto>(program);

            return new OperationResultDto<ProgramBriefResponseDto, GetProgramStatus>()
            {
                Content = content,
                Status = GetProgramStatus.Success
            };
        }

        /// <summary>
        /// Generates list of program events in accordance with provided criteria.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="programId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ProgramScheduleDto> GetProgramSchedule(
            int customerId, 
            Guid programId,
            ProgramScheduleRequestDto model
        )
        {
            var program = await programService.Get(customerId, programId);

            if (program == null)
            {
                return null;
            }

            int startDay = model.StartDay ?? 1;
            int dayIndex = startDay;
            int endDay = program.ProgramElements.Max(pe => pe.ProgramDayElements.Max(pde => pde.Day));

            if (model.EndDay.HasValue && model.EndDay.Value < endDay)
            {
                endDay = model.EndDay.Value;
            }
            
            DateTime startDateUtc = model.StartDateUtc ?? DateTime.UtcNow;
            DateTime dateIndex = DateTime.SpecifyKind(startDateUtc, DateTimeKind.Utc);

            var result = new List<ProgramScheduleEventDto>();

            while (dayIndex <= endDay)
            {
                var dayElements =
                    program.ProgramElements.Where(pe => pe.ProgramDayElements.Any(pde => pde.Day == dayIndex)).ToList();

                if (dayElements.Any())
                {
                    var scheduleEvent = new ProgramScheduleEventDto(model, dayElements, dateIndex, dayIndex)
                    {
                        ProgramDay = dayIndex,
                        Name = program.Name,
                        EventTz = model.StartDateTz
                    };

                    result.Add(scheduleEvent);
                }

                dateIndex = AddDays(dateIndex, model.StartDateTz, 1);
                dayIndex++;
            }

            return new ProgramScheduleDto
            {
                ProgramEvents = result,
                EndDay = endDay,
                StartDay = startDay,
                ExpireMinutes = model.ExpireMinutes,
                ProgramId = programId,
                ProgramName = program.Name,
                StartDateUtc = startDateUtc
            };
        }

        /// <summary>
        /// Adds the days taking into account DST.
        /// </summary>
        /// <param name="targetUtc">The target UTC.</param>
        /// <param name="timeZone">The time zone.</param>
        /// <param name="numberOfDays">The number of days.</param>
        /// <returns></returns>
        private DateTime AddDays(DateTime targetUtc, string timeZone, int numberOfDays)
        {
            Instant instant = Instant.FromDateTimeUtc(targetUtc);

            DateTimeZone timeZoneInfo = DateTimeZoneProviders.Tzdb[timeZone];

            ZonedDateTime zoned = instant.InZone(timeZoneInfo);

            LocalDateTime updated = zoned.LocalDateTime.PlusDays(numberOfDays); // Adding a number of days

            ZonedDateTime updatedZoned = timeZoneInfo.AtLeniently(updated);

            return updatedZoned.ToDateTimeUtc();
        }
    }
}