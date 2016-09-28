using System.Linq;
using System.Threading.Tasks;
using HealthLibrary.Common;
using HealthLibrary.DataAccess;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Domain.Entities.Enums;
using HealthLibrary.DomainLogic.Services.Interfaces;

namespace HealthLibrary.DomainLogic.Services.Implementations
{
    /// <summary>
    /// Provides business logic for open ended answer sets.
    /// </summary>
    public class OpenEndedAnswerSetsService : IOpenEndedAnswerSetsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<AnswerSet> answerSetRepository;

        public OpenEndedAnswerSetsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.answerSetRepository = this.unitOfWork.CreateGenericRepository<AnswerSet>();
        }

        /// <summary>
        /// Returns open ended answer set for specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<AnswerSet> Get(int customerId)
        {
            var answerSets = await this.answerSetRepository.FindAsync(a => a.Type == AnswerSetType.OpenEnded && a.CustomerId == customerId);

            if (!answerSets.Any())
            {
                return await this.InitDefaultOpenEndedAnswerSet(customerId);
            }

            return answerSets.First();
        }

        /// <summary>
        /// Initializes default answer set for customer with specified id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private async Task<AnswerSet> InitDefaultOpenEndedAnswerSet(int customerId)
        {
            var defaultAnswerSet = await this.Get(Settings.CICustomerId);

            var newAnswerSet = new AnswerSet
            {
                CustomerId = customerId,
                Type = defaultAnswerSet.Type,
                Name = defaultAnswerSet.Name
            };

            answerSetRepository.Insert(newAnswerSet);

            await this.unitOfWork.SaveAsync();

            return newAnswerSet;
        }
    }
}
