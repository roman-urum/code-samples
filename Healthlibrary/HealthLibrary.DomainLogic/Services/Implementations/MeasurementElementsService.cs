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
    /// Contains business logic for measurement elements.
    /// </summary>
    public class MeasurementElementsService : IMeasurementElementsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<MeasurementElement> measurementElementsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementElementsService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public MeasurementElementsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.measurementElementsRepository = unitOfWork.CreateGenericRepository<MeasurementElement>();
        }

        /// <summary>
        /// Returns measurement element by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="elementId"></param>
        /// <returns></returns>
        public async Task<MeasurementElement> GetById(int customerId, Guid elementId)
        {
            var result =
                await this.measurementElementsRepository.FindAsync(m => m.CustomerId == customerId && m.Id == elementId);

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Returns list of measurement elements for specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedResult<MeasurementElement>> GetAll(int customerId, TagsSearchDto request = null)
        {
            Expression<Func<MeasurementElement, bool>> expression = m => !m.IsDeleted && m.CustomerId == customerId;

            // TODO: Code below should be removed when initialization of default data will be implemented in correct way
            var measurements = await this.measurementElementsRepository.FindAsync(expression);

            if (!measurements.Any())
            {
                await this.InitDefaultMeasurementElements(customerId);
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

            return await measurementElementsRepository
                .FindPagedAsync(
                    expression,
                    m => m.OrderBy(e => e.MeasurementType).ThenBy(e => e.Name),
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
        public async Task<IList<MeasurementElement>> InitDefaultMeasurementElements(int customerId)
        {
            var ciMeasurements = await this.GetAll(Settings.CICustomerId);
            var result = new List<MeasurementElement>();

            foreach (var measurement in ciMeasurements.Results)
            {
                var entity = new MeasurementElement
                {
                    Type = ElementType.Measurement,
                    MeasurementType = measurement.MeasurementType,
                    Name = measurement.Name,
                    CustomerId = customerId
                };

                this.measurementElementsRepository.Insert(entity);
                result.Add(entity);
            }

            await this.unitOfWork.SaveAsync();

            return result;
        }
    }
}