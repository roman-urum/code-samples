using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Maestro.DataAccess.EF.Extensions;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.HealthLibraryService.Elements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.QuestionElements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.ScaleAnswerSet;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerChoices;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerSet;
using Maestro.DomainLogic.Services.Interfaces;
using Maestro.Web.Areas.Customer.Managers.Interfaces;
using Maestro.Web.Areas.Customer.Models.CareBuilder.QuestionElements;
using Maestro.Web.Areas.Customer.Models.CareBuilder.ScaleAnsweSet;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SearchAnswerSets;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SearchCareElements;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerSet;
using Maestro.Web.Security;
using ScaleAnswerSetResponseDto = Maestro.Domain.Dtos.HealthLibraryService.Elements.ScaleAnswerSet.ScaleAnswerSetResponseDto;

namespace Maestro.Web.Areas.Customer.Managers.Implementations
{
    /// <summary>
    /// CareBuilderControllerManager.QuestionElements.
    /// </summary>
    public partial class CareBuilderControllerManager : ICareBuilderControllerManager
    {
        private readonly IHealthLibraryService healthLibraryService;
        private readonly IVitalsService vitalsService;
        private readonly IAuthDataStorage authDataStorage;
        private readonly ICustomerContext customerContext;
        private readonly NLog.Logger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CareBuilderControllerManager" /> class.
        /// </summary>
        /// <param name="healthLibraryService">The health library service.</param>
        /// <param name="vitalsService"></param>
        /// <param name="authDataStorage">The authentication data storage.</param>
        /// <param name="customerContext">The customer context.</param>
        public CareBuilderControllerManager(
            IHealthLibraryService healthLibraryService,
            IVitalsService vitalsService,
            IAuthDataStorage authDataStorage,
            ICustomerContext customerContext)
        {
            this.healthLibraryService = healthLibraryService;
            this.vitalsService = vitalsService;
            this.authDataStorage = authDataStorage;
            this.customerContext = customerContext;
            this.logger = NLog.LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreateScaleAnswerSet(ScaleAnswerSetRequestViewModel model)
        {
            var token = authDataStorage.GetToken();
            var request = Mapper.Map<ScaleAnswerSetRequestViewModel, CreateScaleAnswerSetRequestDto>(model);

            var result = await healthLibraryService.CreateScaleAnswerSet(request, CustomerContext.Current.Customer.Id, token);

            return result;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreateSelectionAnswerSet(CreateSelectionAnswerSetViewModel request)
        {
            var token = authDataStorage.GetToken();
            var model = Mapper.Map<CreateSelectionAnswerSetViewModel, CreateSelectionAnswerSetRequestDto>(request);

            model.SelectionAnswerChoices = new List<CreateSelectionAnswerChoiceRequestDto>();

            foreach (var answerChoice in request.SelectionAnswerChoices)
            {
                var answerChoiceDto = Mapper.Map<CreateSelectionAnswerChoiceRequestDto>(answerChoice);
                answerChoiceDto.AnswerString = this.InitCreateLocalizedStringRequest(answerChoice.AnswerString);

                model.SelectionAnswerChoices.Add(answerChoiceDto);
            }
            
            var result = await healthLibraryService
                .CreateSelectionAnswerSet(model, CustomerContext.Current.Customer.Id, token);

            return result;
        }

        /// <summary>
        /// Gets the selection answer set.
        /// </summary>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <returns></returns>
        public async Task<SelectionAnswerSetViewModel> GetSelectionAnswerSet(Guid selectionAnswerSetId)
        {
            var token = authDataStorage.GetToken();
            var result = await healthLibraryService
                .GetSelectionAnswerSet(selectionAnswerSetId, CustomerContext.Current.Customer.Id, token);

            return Mapper.Map<SelectionAnswerSetResponseDto, SelectionAnswerSetViewModel>(result);
        }

        /// <summary>
        /// Updates the selection answer set.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public Task UpdateSelectionAnswerSet(UpdateSelectionAnswerSetViewModel request)
        {
            var token = authDataStorage.GetToken();
            var model = Mapper.Map<UpdateSelectionAnswerSetViewModel, UpdateSelectionAnswerSetRequestDto>(request);

            model.SelectionAnswerChoices = new List<UpdateSelectionAnswerChoiceRequestDto>();

            foreach (var answerChoice in request.SelectionAnswerChoices)
            {
                var answerChoiceDto = Mapper.Map<UpdateSelectionAnswerChoiceRequestDto>(answerChoice);
                answerChoiceDto.AnswerString = this.InitUpdateLocalizedStringRequest(answerChoice.AnswerString);

                model.SelectionAnswerChoices.Add(answerChoiceDto);
            }

            return healthLibraryService.UpdateSelectionAnswerSet(model, CustomerContext.Current.Customer.Id, token);
        }

        /// <summary>
        /// Finds the answer sets.
        /// </summary>
        /// <param name="searchAnswerSetModel">The search answer set model.</param>
        /// <returns></returns>
        public async Task<SearchAnswerSetResultDto> FindAnswerSets(SearchAnswerSetViewModel searchAnswerSetModel)
        {
            var searchResult = new SearchAnswerSetResultDto()
            {
                SelectionAnswerSets = new List<UpdateSelectionAnswerSetViewModel>(),
                ScaleAnswerSets = new List<SearchScaleAnswerSetResultViewModel>()
            };

            var searchScaleAnswersetRequest = new SearchRequestDto()
            {
                CustomerId = this.customerContext.Customer.Id,
                Q = searchAnswerSetModel.Keyword,
                Tags = new string[] { searchAnswerSetModel.Tag }
            };

            var searchSelectionAnswerSetRequest = new SearchSelectionRequestDto()
            {
                CustomerId = this.customerContext.Customer.Id,
                Q = searchAnswerSetModel.Keyword,
                Tags = new[] { searchAnswerSetModel.Tag },
                IsMultipleChoice = false
            };
            var searchCheckListAnswerSetRequest = new SearchSelectionRequestDto()
            {
                CustomerId = this.customerContext.Customer.Id,
                Q = searchAnswerSetModel.Keyword,
                Tags = new[] { searchAnswerSetModel.Tag },
                IsMultipleChoice = true
            };

            string authToken = this.authDataStorage.GetToken();

            if (searchAnswerSetModel.SearchAnswerSetType == SearchAnswerSetType.All)
            {
                var mappedSelectionAnswerSets =
                    (await this.healthLibraryService.FindSelectionAnswerSets(searchSelectionAnswerSetRequest, authToken))
                        .Select(Mapper.Map<UpdateSelectionAnswerSetViewModel>);
                searchResult.SelectionAnswerSets.AddRange(mappedSelectionAnswerSets);
                var mappedChecklistAnswerSets =
                    (await this.healthLibraryService.FindSelectionAnswerSets(searchCheckListAnswerSetRequest, authToken))
                        .Select(Mapper.Map<UpdateSelectionAnswerSetViewModel>);
                searchResult.SelectionAnswerSets.AddRange(mappedChecklistAnswerSets);

                var mappedScaleAnswerSets =
                    (await this.healthLibraryService.FindScaleAnswerSets(searchScaleAnswersetRequest, authToken)).Select
                        (Mapper.Map<SearchScaleAnswerSetResultViewModel>);
                searchResult.ScaleAnswerSets.AddRange(mappedScaleAnswerSets);
            }
            else if (searchAnswerSetModel.SearchAnswerSetType == SearchAnswerSetType.CheckList)
            {
                var mappedChecklistAnswerSets =
                    (await this.healthLibraryService.FindSelectionAnswerSets(searchCheckListAnswerSetRequest, authToken))
                        .Select(Mapper.Map<UpdateSelectionAnswerSetViewModel>);
                searchResult.SelectionAnswerSets.AddRange(mappedChecklistAnswerSets);
            }
            else if (searchAnswerSetModel.SearchAnswerSetType == SearchAnswerSetType.MultipleChoice)
            {
                var mappedSelectionAnswerSets =
                    (await this.healthLibraryService.FindSelectionAnswerSets(searchSelectionAnswerSetRequest, authToken))
                        .Select(Mapper.Map<UpdateSelectionAnswerSetViewModel>);
                searchResult.SelectionAnswerSets.AddRange(mappedSelectionAnswerSets);
            }
            else if (searchAnswerSetModel.SearchAnswerSetType == SearchAnswerSetType.ScaleAnswerSet)
            {
                var scaleAnswerSets =
                    await this.healthLibraryService.FindScaleAnswerSets(searchScaleAnswersetRequest, authToken);
                var mappedScaleAnswerSets = scaleAnswerSets.Select(Mapper.Map<SearchScaleAnswerSetResultViewModel>);
                searchResult.ScaleAnswerSets.AddRange(mappedScaleAnswerSets);
            }

            return searchResult;
        }

        /// <summary>
        /// Gets the open ended answer set.
        /// </summary>
        /// <returns></returns>
        public async Task<OpenEndedAnswerSetResponseDto> GetOpenEndedAnswerSet()
        {
            var token = this.authDataStorage.GetToken();

            return await this.healthLibraryService.GetOpenEndedAnswerSet(this.customerContext.Customer.Id, token);
        }

        /// <summary>
        /// Creates new question element.
        /// </summary>
        /// <param name="createQuestionModel"></param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreateQuestionElement(CreateQuestionElementViewModel createQuestionModel)
        {
            var token = this.authDataStorage.GetToken();
            var createQuestionDto = Mapper.Map<CreateQuestionElementRequestDto>(createQuestionModel);

            createQuestionDto.QuestionElementString =
                this.InitCreateLocalizedStringRequest(createQuestionModel.QuestionElementString);

            return await this.healthLibraryService.CreateQuestion(createQuestionDto, this.customerContext.Customer.Id, token);
        }

        /// <summary>
        /// Gets the scale answer set.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<ScaleAnswerSetResponseViewModel> GetScaleAnswerSet(Guid id)
        {
            var token = authDataStorage.GetToken();

            var result = await healthLibraryService.GetScaleAnswerSet(id,
                CustomerContext.Current.Customer.Id, token);

            return Mapper.Map<ScaleAnswerSetResponseDto, ScaleAnswerSetResponseViewModel>(result);
        }

        /// <summary>
        /// Updates the scale answer set.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task UpdateScaleAnswerSet(ScaleAnswerSetUpdateRequestViewModel request)
        {
            var token = authDataStorage.GetToken();
            var model = Mapper.Map<ScaleAnswerSetUpdateRequestViewModel, UpdateScaleAnswerSetRequestDto>(request);

            await healthLibraryService.UpdateScaleAnswerSet(model, CustomerContext.Current.Customer.Id, token);
        }

        /// <summary>
        /// Updates the question element.
        /// </summary>
        /// <param name="updateQuestionElementViewModel">The update question element view model.</param>
        /// <returns></returns>
        public async Task UpdateQuestionElement(UpdateQuestionElementViewModel updateQuestionElementViewModel)
        {
            var updateQuestionElementDto = Mapper.Map<UpdateQuestionElementRequestDto>(updateQuestionElementViewModel);
            var token = authDataStorage.GetToken();

            updateQuestionElementDto.QuestionElementString =
                this.InitUpdateLocalizedStringRequest(updateQuestionElementViewModel.QuestionElementString);

            await this.healthLibraryService.UpdateQuestionElement(updateQuestionElementDto, this.customerContext.Customer.Id, token);
        }

        /// <summary>
        /// Gets the question element.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<QuestionElementResponseDto> GetQuestionElement(Guid id)
        {
            var token = this.authDataStorage.GetToken();

            var questionElementDto = await this.healthLibraryService.GetQuestionElement(token, CustomerContext.Current.Customer.Id, id, false);

            //var questionElementModel = Mapper.Map<QuestionProtocolElementResponseViewModel>(questionElementDto);

            return questionElementDto;
        }

        /// <summary>
        /// Returns list of selection answer sets which matches to search criteria.
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UpdateSelectionAnswerSetViewModel>> FindSelectionAnswerSets(
            SearchCareElementsViewModel searchModel)
        {
            var searchRequest = new SearchSelectionRequestDto
            {
                IsMultipleChoice = false,
                Q = searchModel.Keyword,
                CustomerId = CustomerContext.Current.Customer.Id
            };

            var result = await this.healthLibraryService
                .FindSelectionAnswerSets(
                    searchRequest,
                    authDataStorage.GetToken()
                );

            return result.Select(Mapper.Map<UpdateSelectionAnswerSetViewModel>).ToList();
        }

        /// <summary>
        /// Returns list of scale answer sets which matches to search criteria.
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ScaleAnswerSetResponseViewModel>> FindScaleAnswerSets(
            SearchCareElementsViewModel searchModel)
        {
            var searchRequest = new SearchRequestDto
            {
                Q = searchModel.Keyword,
                CustomerId = CustomerContext.Current.Customer.Id
            };

            var result = await this.healthLibraryService.FindScaleAnswerSets(
                searchRequest, this.authDataStorage.GetToken());

            return result.Select(Mapper.Map<ScaleAnswerSetResponseViewModel>).ToList();
        }

        /// <summary>
        /// Returns list of question elements which matches to search criteria.
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public async Task<IEnumerable<QuestionElementListItemViewModel>> FindQuestionElements(
            SearchCareElementsViewModel searchModel)
        {
            var searchRequest = new SearchRequestDto
            {
                Q = searchModel.Keyword,
                CustomerId = CustomerContext.Current.Customer.Id
            };

            var result = await this.healthLibraryService.FindQuestionElements(searchRequest, authDataStorage.GetToken());

            return result.Select(Mapper.Map<QuestionElementListItemViewModel>).ToList();
        }
    }
}