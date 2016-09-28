using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Web.Areas.Customer.Models.CareBuilder.MeasurementElements;

namespace Maestro.Web.Areas.Customer.Managers.Interfaces
{
    public partial interface ICareBuilderControllerManager
    {
        /// <summary>
        /// Returns all measurements for specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<IList<MeasurementElementViewModel>> GetMeasurementElements(int customerId);

        /// <summary>
        /// Returns measurement by id.
        /// </summary>
        /// <returns></returns>
        Task<MeasurementElementViewModel> GetMeasurementElement(int customerId, Guid elementId);
    }
}