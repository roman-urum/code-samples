using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.HealthLibraryService.Elements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.QuestionElements;
using Maestro.Web.Areas.Customer.Models.CareBuilder.QuestionElements;
using Maestro.Web.Areas.Customer.Models.CareBuilder.ScaleAnsweSet;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SearchAnswerSets;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SearchCareElements;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerSet;

namespace Maestro.Web.Areas.Customer.Managers.Interfaces
{
    /// <summary>
    /// ICareBuilderControllerManager.QuestionElements.
    /// </summary>
    public partial interface ICareBuilderControllerManager
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateScaleAnswerSet(ScaleAnswerSetRequestViewModel model);

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateSelectionAnswerSet(CreateSelectionAnswerSetViewModel request);

        /// <summary>
        /// Gets the selection answer set.
        /// </summary>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <returns></returns>
        Task<SelectionAnswerSetViewModel> GetSelectionAnswerSet(Guid selectionAnswerSetId);

        /// <summary>
        /// Updates the selection answer set.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task UpdateSelectionAnswerSet(UpdateSelectionAnswerSetViewModel request);

        /// <summary>
        /// Finds the answer sets.
        /// </summary>
        /// <param name="searchAnswerModel">The search answer model.</param>
        /// <returns></returns>
        Task<SearchAnswerSetResultDto> FindAnswerSets(SearchAnswerSetViewModel searchAnswerModel);

        /// <summary>
        /// Creates the question element.
        /// </summary>
        /// <param name="createQuestionModel">The create question model.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateQuestionElement(CreateQuestionElementViewModel createQuestionModel);
        
        /// <summary>
        /// Gets the scale answer set.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<ScaleAnswerSetResponseViewModel> GetScaleAnswerSet(Guid id);

        /// <summary>
        /// Updates the scale answer set.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task UpdateScaleAnswerSet(ScaleAnswerSetUpdateRequestViewModel model);

        /// <summary>
        /// Gets the open ended answer set.
        /// </summary>
        /// <returns></returns>
        Task<OpenEndedAnswerSetResponseDto> GetOpenEndedAnswerSet();

        /// <summary>
        /// Updates the question element.
        /// </summary>
        /// <param name="updateQuestionElementViewModel">The update question element view model.</param>
        /// <returns></returns>
        Task UpdateQuestionElement(UpdateQuestionElementViewModel updateQuestionElementViewModel);

        /// <summary>
        /// Gets the question element.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<QuestionElementResponseDto> GetQuestionElement(Guid id);

        /// <summary>
        /// Returns list of selection answer sets which matches to search criteria.
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        Task<IEnumerable<UpdateSelectionAnswerSetViewModel>> FindSelectionAnswerSets(SearchCareElementsViewModel searchModel);

        /// <summary>
        /// Returns list of scale answer sets which matches to search criteria.
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        Task<IEnumerable<ScaleAnswerSetResponseViewModel>> FindScaleAnswerSets(SearchCareElementsViewModel searchModel);

        /// <summary>
        /// Returns list of question elements which matches to search criteria.
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        Task<IEnumerable<QuestionElementListItemViewModel>> FindQuestionElements(
            SearchCareElementsViewModel searchModel);
    }
}