using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HealthLibrary.Common;
using HealthLibrary.Common.Helpers;
using HealthLibrary.DataAccess;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Domain.Entities.Enums;
using HealthLibrary.DomainLogic.Services.Interfaces;

namespace HealthLibrary.DomainLogic.Services.Implementations
{
    /// <summary>
    /// Contains business logic for Assessment elements.
    /// </summary>
    public class AssessmentElementsService : IAssessmentElementsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<AssessmentElement> assessmentElementsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentElementsService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public AssessmentElementsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.assessmentElementsRepository = unitOfWork.CreateGenericRepository<AssessmentElement>();
        }

        /// <summary>
        /// Returns Assessment element by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="elementId"></param>
        /// <returns></returns>
        public async Task<AssessmentElement> GetById(int customerId, Guid elementId)
        {
            var result =
                await this.assessmentElementsRepository.FindAsync(m => m.CustomerId == customerId && m.Id == elementId);

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Returns list of Assessment elements for specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedResult<AssessmentElement>> GetAll(int customerId, TagsSearchDto request = null)
        {
            Expression<Func<AssessmentElement, bool>> expression = m => !m.IsDeleted && m.CustomerId == customerId;

            // TODO: Code below should be removed when initialization of default data will be implemented in correct way
            var assessments = await this.assessmentElementsRepository.FindAsync(expression);

            if (!assessments.Any())
            {
                await this.InitDefaultAssessmentElements(customerId);
            }

            // TODO: Code above should be removed when initialization of default data will be implemented in correct way
            if (request != null)
            {
                if (request.Tags != null && request.Tags.Any())
                {
                    foreach (var tag in request.Tags)
                    {
                        expression = expression.Or(m => m.Tags.Any(t => t.Name.ToLower() == tag.ToLower()));
                    }
                }

                if (!string.IsNullOrEmpty(request.Q))
                {
                    var terms = request.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(m => m.Name.Contains(term));
                    }
                }
            }

            return await assessmentElementsRepository
                .FindPagedAsync(
                    expression,
                    m => m.OrderBy(e => e.AssessmentTypeString).ThenBy(e => e.Name),
                    null,
                    request != null ? request.Skip : (int?)null,
                    request != null ? request.Take : (int?)null
                );
        }

        /// <summary>
        /// Copies list of elements from CI customer to specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<IList<AssessmentElement>> InitDefaultAssessmentElements(int customerId)
        {
            var ciAssessments = await this.GetAll(Settings.CICustomerId);
            var result = new List<AssessmentElement>();

            foreach (var assessment in ciAssessments.Results)
            {
                var entity = new AssessmentElement
                {
                    Type = ElementType.Assessment,
                    AssessmentType = assessment.AssessmentType,
                    Name = assessment.Name,
                    CustomerId = customerId
                };

                this.assessmentElementsRepository.Insert(entity);
                result.Add(entity);
            }

            await this.unitOfWork.SaveAsync();

            return result;
        }
    }
}