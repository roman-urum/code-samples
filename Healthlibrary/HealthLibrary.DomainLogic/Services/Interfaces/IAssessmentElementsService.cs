using System;
using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// Declaration of public methods for assessments service.
    /// </summary>
    public interface IAssessmentElementsService
    {
        /// <summary>
        /// Returns Assessment element by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="elementId"></param>
        /// <returns></returns>
        Task<AssessmentElement> GetById(int customerId, Guid elementId);

        /// <summary>
        /// Returns list of Assessment elements for specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedResult<AssessmentElement>> GetAll(int customerId, TagsSearchDto request = null);
    }
}
