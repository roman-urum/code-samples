using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Maestro.Web.Areas.Customer.Models.CareBuilder.MeasurementElements;

namespace Maestro.Web.Areas.Customer.Managers.Implementations
{
    public partial class CareBuilderControllerManager
    {
        /// <summary>
        /// Returns all measurements for specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<IList<MeasurementElementViewModel>> GetMeasurementElements(int customerId)
        {
            var result = await healthLibraryService.GetMeasurementElements(customerId, this.authDataStorage.GetToken());

            return result.Select(Mapper.Map<MeasurementElementViewModel>).ToList();
        }

        /// <summary>
        /// Returns measurement dto by element id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="elementId"></param>
        /// <returns></returns>
        public async Task<MeasurementElementViewModel> GetMeasurementElement(int customerId, Guid elementId)
        {
            var result =
                await healthLibraryService.GetMeasurementElement(customerId, elementId, this.authDataStorage.GetToken());

            if (result == null)
            {
                return null;
            }

            return Mapper.Map<MeasurementElementViewModel>(result);
        }
    }
}