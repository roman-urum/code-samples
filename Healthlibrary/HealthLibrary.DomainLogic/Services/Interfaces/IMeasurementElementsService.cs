using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// Contains business logic for measurement elements.
    /// </summary>
    public interface IMeasurementElementsService
    {
        /// <summary>
        /// Returns measurement element by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="elementId"></param>
        /// <returns></returns>
        Task<MeasurementElement> GetById(int customerId, Guid elementId);

        /// <summary>
        /// Returns list of measurement elements for specified customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResult<MeasurementElement>> GetAll(int customerId, TagsSearchDto request = null);

        /// <summary>
        /// Copies list of elements from CI customer to specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<IList<MeasurementElement>> InitDefaultMeasurementElements(int customerId);
    }
}