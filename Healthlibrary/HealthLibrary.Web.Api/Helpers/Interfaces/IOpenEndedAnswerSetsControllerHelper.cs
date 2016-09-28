using System.Threading.Tasks;
using HealthLibrary.Web.Api.Models.Elements;

namespace HealthLibrary.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// Helper to generate response models for open ended answer sets.
    /// </summary>
    public interface IOpenEndedAnswerSetsControllerHelper
    {
        /// <summary>
        /// Returns response model with open ended answer set for specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<OpenEndedAnswerSetResponseDto> Get(int customerId);
    }
}
