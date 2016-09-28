using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Maestro.Web.Areas.Customer.Models.CareBuilder.AssessmentElements;

namespace Maestro.Web.Areas.Customer.Managers.Implementations
{
    public partial class CareBuilderControllerManager
    {
        /// <summary>
        /// Returns all assessments for specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<IList<AssessmentElementViewModel>> GetAssessmentElements(int customerId)
        {
            var result = await healthLibraryService.GetAssessmentElements(customerId, this.authDataStorage.GetToken());

            return result.Select(Mapper.Map<AssessmentElementViewModel>).ToList();
        }

        /// <summary>
        /// Returns assessment dto by element id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="elementId"></param>
        /// <returns></returns>
        public async Task<AssessmentElementViewModel> GetAssessmentElement(int customerId, Guid elementId)
        {
            var result =
                await healthLibraryService.GetAssessmentElement(customerId, elementId, this.authDataStorage.GetToken());

            if (result == null)
            {
                return null;
            }

            return Mapper.Map<AssessmentElementViewModel>(result);
        }
    }
}