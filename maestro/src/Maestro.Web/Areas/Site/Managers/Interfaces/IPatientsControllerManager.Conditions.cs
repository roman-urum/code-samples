using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Maestro.Domain.Dtos.VitalsService.Conditions;
using Maestro.Web.Areas.Site.Models.Patients.PatientsConditions;

namespace Maestro.Web.Areas.Site.Managers.Interfaces
{
    public partial interface IPatientsControllerManager
    {
        Task<IList<ConditionResponseDto>> GetPatientConditions(Guid patientId);

        /// <summary>
        /// Creates the patient conditions.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task CreatePatientConditions(PatiensConditionsRequestViewModel request);
    }
}