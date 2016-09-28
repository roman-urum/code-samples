using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Domain.Entities.Program;
using HealthLibrary.Domain.Entities.Protocol;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models;

namespace HealthLibrary.Web.Api.Helpers.Implementations
{
    /// <summary>
    /// GlobalSearchCacheHelper.
    /// </summary>
    public class GlobalSearchCacheHelper : IGlobalSearchCacheHelper
    {
        private const string SearchIndexKeyTemplate = "SEARCH-INDEX-{0}";

        private readonly ICacheProvider cacheProvider;
        private readonly IProgramService programService;
        private readonly IProtocolService protocolService;
        private readonly IMeasurementElementsService measurementElementsService;
        private readonly IAssessmentElementsService assessmentElementsService;
        private readonly IQuestionElementService questionElementService;
        private readonly ITextMediaElementsService textMediaElementsService;
        private readonly IScaleAnswerSetService scaleAnswerSetService;
        private readonly ISelectionAnswerSetService selectionAnswerSetService;
        private readonly ICareElementRequestContext careElementRequestContext;
        private readonly IOpenEndedAnswerSetsService openEndedAnswerSetsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchControllerHelper" /> class.
        /// </summary>
        /// <param name="cacheProvider">The cache provider.</param>
        /// <param name="programService">The program service.</param>
        /// <param name="protocolService">The protocol service.</param>
        /// <param name="measurementElementsService">The measurement elements service.</param>
        /// <param name="assessmentElementsService"></param>
        /// <param name="questionElementService">The question element service.</param>
        /// <param name="textMediaElementsService">The text media elements service.</param>
        /// <param name="scaleAnswerSetService">The scale answer set service.</param>
        /// <param name="selectionAnswerSetService">The selection answer set service.</param>
        /// <param name="careElementRequestContext">The care element request context.</param>
        /// <param name="openEndedAnswerSetsService">The open ended answer set service</param>
        public GlobalSearchCacheHelper(
            ICacheProvider cacheProvider,
            IProgramService programService,
            IProtocolService protocolService,
            IMeasurementElementsService measurementElementsService,
            IAssessmentElementsService assessmentElementsService,
            IQuestionElementService questionElementService,
            ITextMediaElementsService textMediaElementsService,
            IScaleAnswerSetService scaleAnswerSetService,
            ISelectionAnswerSetService selectionAnswerSetService,
            ICareElementRequestContext careElementRequestContext,
            IOpenEndedAnswerSetsService openEndedAnswerSetsService
        )
        {
            this.cacheProvider = cacheProvider;
            this.programService = programService;
            this.protocolService = protocolService;
            this.measurementElementsService = measurementElementsService;
            this.assessmentElementsService = assessmentElementsService;
            this.questionElementService = questionElementService;
            this.textMediaElementsService = textMediaElementsService;
            this.scaleAnswerSetService = scaleAnswerSetService;
            this.selectionAnswerSetService = selectionAnswerSetService;
            this.careElementRequestContext = careElementRequestContext;
            this.openEndedAnswerSetsService = openEndedAnswerSetsService;
        }

        /// <summary>
        /// Gets all cached entries.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public async Task<ConcurrentDictionary<Guid, SearchEntryDto>> GetAllCachedEntries(int customerId)
        {
            var cacheKey = string.Format(SearchIndexKeyTemplate, customerId);

            var allCachedEntries =
                await cacheProvider.Get<ConcurrentDictionary<Guid, SearchEntryDto>>(
                    cacheKey, async () =>
                    {
                        var result = new ConcurrentDictionary<Guid, SearchEntryDto>();

                        var programs = (await programService.FindPrograms(customerId)).Results;
                        var programsResponses = Mapper.Map<IList<Program>, IList<SearchProgramResponseDto>>(programs);

                        foreach (var program in programsResponses)
                        {
                            result.TryAdd(program.Id, program);
                        }

                        var protocols = Mapper.Map<IList<Protocol>, IList<SearchEntryDto>>((await protocolService.GetProtocols(customerId)).Results);

                        foreach (var protocol in protocols)
                        {
                            result.TryAdd(protocol.Id, protocol);
                        }

                        var measurementElements = Mapper.Map<IList<MeasurementElement>, IList<SearchEntryDto>>((await measurementElementsService.GetAll(customerId)).Results);

                        foreach (var measurementElement in measurementElements)
                        {
                            result.TryAdd(measurementElement.Id, measurementElement);
                        }

                        var assessmentElements = Mapper.Map<IList<AssessmentElement>, IList<SearchEntryDto>>((await assessmentElementsService.GetAll(customerId)).Results);

                        foreach (var assessmentElement in assessmentElements)
                        {
                            result.TryAdd(assessmentElement.Id, assessmentElement);
                        }

                        var questionElements = Mapper.Map<IList<QuestionElement>, IList<SearchEntryDto>>((await questionElementService.Find(customerId)).Results);

                        foreach (var questionElement in questionElements)
                        {
                            result.TryAdd(questionElement.Id, questionElement);
                        }

                        var textMediaElements = Mapper.Map<IList<TextMediaElement>, IList<SearchTextAndMediaDto>>((await textMediaElementsService.GetElements(customerId)).Results);

                        foreach (var textMediaElement in textMediaElements)
                        {
                            result.TryAdd(textMediaElement.Id, textMediaElement);
                        }

                        var scaleAnswerSets = Mapper.Map<IList<ScaleAnswerSet>, IList<SearchEntryDto>>((await scaleAnswerSetService.Find(customerId)).Results);

                        foreach (var scaleAnswerSet in scaleAnswerSets)
                        {
                            result.TryAdd(scaleAnswerSet.Id, scaleAnswerSet);
                        }

                        var selectionAnswerSets = Mapper.Map<IList<SelectionAnswerSet>, IList<SearchEntryDto>>((await selectionAnswerSetService.Find(customerId)).Results);

                        foreach (var selectionAnswerSet in selectionAnswerSets)
                        {
                            result.TryAdd(selectionAnswerSet.Id, selectionAnswerSet);
                        }

                        var openEndedAnswerSet = Mapper.Map<AnswerSet, SearchEntryDto>(await openEndedAnswerSetsService.Get(customerId));

                        result.TryAdd(openEndedAnswerSet.Id, openEndedAnswerSet);

                        return result;
                    }
                );

            return allCachedEntries;
        }

        /// <summary>
        /// Tries to update entry.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        public async Task AddOrUpdateEntry(int customerId, SearchEntryDto entry)
        {
            var cachedEntries = await GetAllCachedEntries(customerId);

            cachedEntries.AddOrUpdate(entry.Id, entry, (key, oldValue) => entry);
        }

        /// <summary>
        /// Removes the entry.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="entryId">The entry identifier.</param>
        /// <returns></returns>
        public async Task RemoveEntry(int customerId, Guid entryId)
        {
            var cachedEntries = await GetAllCachedEntries(customerId);

            SearchEntryDto removedEntry = null;

            cachedEntries.TryRemove(entryId, out removedEntry);
        }
    }
}