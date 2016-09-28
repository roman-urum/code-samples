using System.Threading.Tasks;
using AutoMapper;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models.Elements;

namespace HealthLibrary.Web.Api.Helpers.Implementations
{
    /// <summary>
    /// Helper to generate response models for open ended answer sets.
    /// </summary>
    public class OpenEndedAnswerSetsControllerHelper : IOpenEndedAnswerSetsControllerHelper
    {
        private readonly IOpenEndedAnswerSetsService openEndedAnswerSetService;

        public OpenEndedAnswerSetsControllerHelper(IOpenEndedAnswerSetsService openEndedAnswerSetService)
        {
            this.openEndedAnswerSetService = openEndedAnswerSetService;
        }

        /// <summary>
        /// Returns response model with open ended answer set for specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<OpenEndedAnswerSetResponseDto> Get(int customerId)
        {
            var result = await this.openEndedAnswerSetService.Get(customerId);

            return Mapper.Map<OpenEndedAnswerSetResponseDto>(result);
        }
    }
}