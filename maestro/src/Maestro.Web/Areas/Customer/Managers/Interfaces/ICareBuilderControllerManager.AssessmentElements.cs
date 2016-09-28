using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Web.Areas.Customer.Models.CareBuilder.AssessmentElements;

namespace Maestro.Web.Areas.Customer.Managers.Interfaces
{
    public partial interface ICareBuilderControllerManager
    {
        /// <summary>
        /// Returns all assessments for specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<IList<AssessmentElementViewModel>> GetAssessmentElements(int customerId);

        /// <summary>
        /// Returns assessment by id.
        /// </summary>
        /// <returns></returns>
        Task<AssessmentElementViewModel> GetAssessmentElement(int customerId, Guid elementId);
    }
}