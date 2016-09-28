using System.Threading.Tasks;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// Provides business logic for open ended answer sets.
    /// </summary>
    public interface IOpenEndedAnswerSetsService
    {
        /// <summary>
        /// Returns open ended answer set for specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<AnswerSet> Get(int customerId);
    }
}
